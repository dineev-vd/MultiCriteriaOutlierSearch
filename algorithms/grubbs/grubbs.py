from flask import request, jsonify, Flask
from outliers import smirnov_grubbs as gr
import json

app = Flask(__name__)

@app.route('/algorithms/grubbs/', methods=['POST'])
def grubbs():
    try:
        data = request.json["Data"]
        params = request.json["Params"]
        alpha = params["alpha"]
        indices = gr.two_sided_test_indices(data,alpha=alpha)
        a = [0 for i in range(0,len(data))]
        for i in indices:
            a[i] = 1
    except:
        return jsonify("error:grubbs"), 400
    return jsonify(a)

@app.route('/algorithms/grubbs/config/',methods=['GET'])
def config():
    with open("config.json") as json_file:
        data = json.load(json_file)
    return jsonify(data)

if __name__ == "__main__":
    app.run()
    