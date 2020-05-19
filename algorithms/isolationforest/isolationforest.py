import codecs
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

        n_estimators = 100
        max_samples = "auto"
        contamination = "auto"

        if "n_estimators" in params:
            n_estimators = params["n_estimators"]

        if "max_samples" in params:
            max_samples = params["max_samples"]

        if "contamination" in params:
            contamination = params["contamination"]


        clf = IsolationForest(n_estimators=n_estimators, max_samples=max_samples, contamination=contamination)


        indices = clf.fit_predict(data)
        indices = [0 if x==1 else 1 for x in indices.tolist()]
        if params['ReturnDoubles']:
            indices = -clf.score_samples(data)
            indices = indices.tolist()
        return jsonify({"message": "OK", "data": indices})
    except Exception as e:
        return jsonify({"message": str(e)}), 400



@app.route('/algorithms/isolationforest/config/', methods=['GET'])
def config():
    with codecs.open("config.json",encoding='utf8') as json_file:
        data = json.load(json_file)
    return jsonify(data)


if __name__ == "__main__":
    app.run(debug=True, port=7000)