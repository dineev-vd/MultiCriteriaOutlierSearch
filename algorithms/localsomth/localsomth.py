import codecs
import json
from flask import Flask, request, jsonify
from sklearn.neighbors import LocalOutlierFactor
import numpy as np

app = Flask(__name__)

@app.route('/algorithms/localsomth/', methods=['POST'])
def dixon():
    try:
        data = np.array(request.json["Data"])
        params = request.json['Params']

        n_neighbors = 20
        contamination = "auto"

        if "n_neighbors" in params:
            n_neighbors = params["n_neighbors"]

        if "contamination" in params:
            contamination = params["contamination"]

        clf = LocalOutlierFactor(n_neighbors=n_neighbors, contamination=contamination)


        indices = clf.fit_predict(X=data)
        indices = [1 if x==1 else 0 for x in indices.tolist()]
        neg = clf.negative_outlier_factor_
        pos = -1 * neg
        maxim = max(pos)
        transformed = pos / float(maxim)
        if params['return_doubles']:
            indices = transformed.tolist()
    except Exception as e:
        return jsonify({"message": str(e)}), 400
    return jsonify({"message": "OK", "data": indices})


@app.route('/algorithms/localsomth/config/', methods=['GET'])
def config():
    with codecs.open("config.json",encoding='utf8') as json_file:
        data = json.load(json_file)
    return jsonify(data)


if __name__ == "__main__":
    app.run(debug=True, port=7000)