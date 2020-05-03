import codecs

from flask import request, jsonify, Flask
from outliers import smirnov_grubbs as gr
import json
import numpy as np

app = Flask(__name__)

@app.route('/algorithms/grubbs/', methods=['POST'])
def grubbs():
    try:
        data = np.array(request.json["Data"])
        if data.shape != (len(data), 1):
            raise Exception("Критерий Граббса не поддерживает размерность данных большую, чем 1")

        data = data.reshape(1,-1).flatten()
        params = request.json["Params"]
        alpha = params["alpha"]
        method = params["method"]

        indices = []

        if method == "double_sided":
            indices = gr.two_sided_test_indices(data,alpha=alpha)
        
        if method == "left_sided":
            indices = gr.min_test_indices(data, alpha=alpha)

        if method == "right_sided":
            indices = gr.max_test_indices(data, alpha=alpha)

        a = [0 for i in range(0,len(data))]
        for i in indices:
            a[i] = 1
    except Exception as e:
        return jsonify({"message":str(e)}), 400
    return jsonify({"message":"OK", "data":a})

@app.route('/algorithms/grubbs/config/',methods=['GET'])
def config():
    with codecs.open("config.json",encoding='utf8') as json_file:
        data = json.load(json_file)
    return jsonify(data)

if __name__ == "__main__":
    app.run()
    