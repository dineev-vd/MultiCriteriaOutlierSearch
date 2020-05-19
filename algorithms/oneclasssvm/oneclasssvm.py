import codecs
import json
from flask import Flask, request, jsonify
from sklearn.svm import OneClassSVM
import numpy as np

app = Flask(__name__)

@app.route('/algorithms/oneclasssvm/', methods=['POST'])
def dixon():
    try:
        data = np.array(request.json["Data"])
        params = request.json['Params']

        kernel = "rbf"
        degree = 3
        gamma = 'scale'
        coef = 0

        if "kernel" in params:
            kernel = params["kernel"]

        if "degree" in params:
            degree = params["degree"]

        if "gamma" in params:
            gamma = params["gamma"]

        if "coef" in params:
            coef = params["coef"]

        clf = OneClassSVM(kernel=kernel, degree=degree, gamma=gamma, coef0=coef)


        indices = clf.fit_predict(data)
        indices = [0 if x==1 else 1 for x in indices.tolist()]

    except Exception as e:
        return jsonify({"message": str(e)}), 400
    return jsonify({"message": "OK", "data": indices})


@app.route('/algorithms/oneclasssvm/config/', methods=['GET'])
def config():
    with codecs.open("config.json",encoding='utf8') as json_file:
        data = json.load(json_file)
    return jsonify(data)


if __name__ == "__main__":
    app.run(debug=True, port=7000)