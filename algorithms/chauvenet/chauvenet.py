import codecs
from scipy.special import erfc
from flask import request, Flask, jsonify
import json
import numpy as np


def chauvenet(x, y, mean=None, stdv=None):
   if mean is None:
      mean = y.mean()           # Mean of incoming array y
   if stdv is None:
      stdv = y.std()            # Its standard deviation
   N = len(y)                   # Lenght of incoming arrays
   criterion = 1.0/(2*N)        # Chauvenet's criterion
   d = abs(y-mean)/stdv         # Distance of a value to mean in stdv's
   d /= 2.0**0.5                # The left and right tail threshold values
   prob = erfc(d)               # Area normal dist.
   filter = [0 if pr >= criterion else 1 for pr in prob]    # The 'accept' filter array with booleans
   return filter                # Use boolean array outside this function

app = Flask(__name__)

@app.route('/algorithms/chauvenet/', methods=['POST'])
def dixon():
    try:
        data = np.array(request.json["Data"])
        if data.shape != (len(data), 1):
            raise Exception("Критерий Шовене не поддерживает размерность данных большую, чем 1")

        data = data.reshape(1, -1).flatten()
        params = request.json['Params']

        indices = chauvenet("",data)
    except Exception as e:
        return jsonify({"message": str(e)}), 400
    return jsonify({"message": "OK", "data": indices})


@app.route('/algorithms/chauvenet/config/', methods=['GET'])
def config():
    with codecs.open("config.json",encoding='utf8') as json_file:
        data = json.load(json_file)
    return jsonify(data)


if __name__ == "__main__":
    app.run(debug=True, port=7000)