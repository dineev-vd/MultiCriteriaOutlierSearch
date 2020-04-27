from flask import request, jsonify, Flask
from pyod.models.combination import average
import numpy as np

app = Flask(__name__)


@app.route('/<path:path>', methods=['POST'])
def majority_get(path):
    try:
        array = request.json['Data']
        result = average(np.transpose(array))
        return jsonify(result.tolist())
    except:
        return jsonify("error"), 400