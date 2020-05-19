import codecs
import json

from flask import request, jsonify, Flask
from pyod.models.combination import aom
import numpy as np

app = Flask(__name__)


@app.route('/combinations/aom/', methods=['POST'])
def majority_get():
    try:
        data = request.json["Data"]
        array = np.array(data['values']).transpose()
        params = request.json['Params']
        n_buckets = 3

        if "n_buckets" in params:
            n_buckets = params["n_buckets"]

        result = aom(array, n_buckets=n_buckets, method="dynamic")
        return jsonify({"data":result.tolist(),"message":"OK"})
    except Exception as e:
        return jsonify({"message":str(e)}), 400

@app.route('/combinations/aom/config/', methods=['GET'])
def config():
    with codecs.open("config.json",encoding='utf8') as json_file:
        data = json.load(json_file)
    return jsonify(data)

if __name__ == "__main__":
    app.run(port=7000, debug=True)