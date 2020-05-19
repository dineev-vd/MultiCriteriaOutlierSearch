from flask import request, jsonify, Flask, Response
import json
import numpy as np

q90 = [0.941, 0.765, 0.642, 0.56, 0.507, 0.468, 0.437,
       0.412, 0.392, 0.376, 0.361, 0.349, 0.338, 0.329,
       0.32, 0.313, 0.306, 0.3, 0.295, 0.29, 0.285, 0.281,
       0.277, 0.273, 0.269, 0.266, 0.263, 0.26
      ]

q95 = [0.97, 0.829, 0.71, 0.625, 0.568, 0.526, 0.493, 0.466,
       0.444, 0.426, 0.41, 0.396, 0.384, 0.374, 0.365, 0.356,
       0.349, 0.342, 0.337, 0.331, 0.326, 0.321, 0.317, 0.312,
       0.308, 0.305, 0.301, 0.29
      ]

q99 = [0.994, 0.926, 0.821, 0.74, 0.68, 0.634, 0.598, 0.568,
       0.542, 0.522, 0.503, 0.488, 0.475, 0.463, 0.452, 0.442,
       0.433, 0.425, 0.418, 0.411, 0.404, 0.399, 0.393, 0.388,
       0.384, 0.38, 0.376, 0.372
       ]

Q90 = {n:q for n,q in zip(range(3,len(q90)+1), q90)}
Q95 = {n:q for n,q in zip(range(3,len(q95)+1), q95)}
Q99 = {n:q for n,q in zip(range(3,len(q99)+1), q99)}

def dixon_test(data, left=True, right=True, q_dict=Q95):

    assert(left or right), 'Хотя бы один из тестов мин или макс должен присутствовать'
    assert(len(data) >= 3), 'Нужно хотя бы 3 значения'
    assert(len(data) <= max(q_dict.keys())), 'Размер выборки слишком большой'

    sdata = sorted(data)
    Q_mindiff, Q_maxdiff = (0,0), (0,0)

    if left:
        Q_min = (sdata[1] - sdata[0])
        try:
            Q_min /= (sdata[-1] - sdata[0])
        except ZeroDivisionError:
            pass
        Q_mindiff = (Q_min - q_dict[len(data)], sdata[0])

    if right:
        Q_max = abs((sdata[-2] - sdata[-1]))
        try:
            Q_max /= abs((sdata[0] - sdata[-1]))
        except ZeroDivisionError:
            pass
        Q_maxdiff = (Q_max - q_dict[len(data)], sdata[-1])

    if not Q_mindiff[0] > 0 and not Q_maxdiff[0] > 0:
        outliers = [None, None]

    elif Q_mindiff[0] == Q_maxdiff[0]:
        outliers = [Q_mindiff[1], Q_maxdiff[1]]

    elif Q_mindiff[0] > Q_maxdiff[0]:
        outliers = [Q_mindiff[1], None]

    else:
        outliers = [None, Q_maxdiff[1]]

    return outliers

app = Flask(__name__)
app.config['JSON_AS_ASCII'] = False

@app.route('/algorithms/dixon/', methods=['POST'])
def dixon():
    try:
        data = np.array(request.json["Data"])
        if data.shape != (len(data), 1):
            raise Exception("Критерий Диксона не поддерживает размерность данных большую, чем 1")

        data = data.reshape(1, -1).flatten()
        params = request.json['Params']

        qdict = Q99

        if params['qdict'] == 'q90':
            qdict = Q90

        if params['qdict'] == 'q95':
            qdict = Q95

        if params['qdict'] == 'q99':
            qdict = Q99 

        outliers = dixon_test(data, left=params['left'], right=params['right'], q_dict=qdict)
        indices = [0 for i in range(0,len(data))]
        for outlier in outliers:
            if outlier == None:
                continue
            for i in [j for j, x in enumerate(data) if x == outlier]:
                indices[i] = 1
        return jsonify({"message": "OK", "data": indices})
    except Exception as e:
        return jsonify({"message":str(e)}), 400


@app.route('/algorithms/dixon/config/',methods=['GET'])
def config():
    try:
        with open("config.json", encoding='utf8') as json_file:
            data = json.load(json_file, encoding='utf8')
            return Response(json.dumps(data, ensure_ascii=False), content_type='application/json; charset=utf-8')
    except Exception as e:
        return jsonify({"Ошибка при чтении файла конфигурации"}), 500

if __name__ == "__main__":
    app.run(debug=True,port=7000)