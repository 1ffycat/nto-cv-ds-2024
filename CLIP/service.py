from CLIP import CLIP
from flask import Flask, request
import pandas as pd


app = Flask(__name__)
app.config['JSON_AS_ASCII'] = False
model = CLIP()
data = pd.read_csv('Tests/photos_v3.csv.xz')


@app.route('/service')
def service():
    args = request.args
    if 'image' in args:
        # Вставить реализацию с картинкой
        pass
    elif 'prompt' in args: 
        img, index = model.get_by_prompt(data['img'], args['prompt'])
        return app.response_class(
            response=str(index),
            status=200,
            mimetype="text"
        )


if __name__ == "__main__":
    app.run()