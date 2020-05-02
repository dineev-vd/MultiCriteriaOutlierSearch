import json

from flask import Flask, request, jsonify
from sklearn.ensemble import IsolationForest
import numpy as np




app = Flask(__name__)

@app.route('/algorithms/isolationforest/', methods=['POST'])
def dixon():
    try:
        data = np.array(request.json["Data"])
        params = request.json['Params']

        clf = IsolationForest()
        indices = clf.fit_predict(data)
    except Exception as e:
        return jsonify({"message": str(e)}), 400
    return jsonify({"message": "OK", "data": indices.tolist()})


@app.route('/algorithms/isolationforest/config/', methods=['GET'])
def config():
    with open("config.json") as json_file:
        data = json.load(json_file)
    return jsonify(data)


if __name__ == "__main__":
    app.run(debug=True, port=7000)