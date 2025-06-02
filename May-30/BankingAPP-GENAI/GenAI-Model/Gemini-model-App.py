from flask import Flask, request, jsonify
import requests
import os

app = Flask(__name__)

#api template
# curl "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=GEMINI_API_KEY" \
#   -H 'Content-Type: application/json' \
#   -X POST \
#   -d '{
#     "contents": [
#       {
#         "parts": [
#           {
#             "text": "Explain how AI works in a few words"
#           }
#         ]
#       }
#     ]
#   }'
# Set your Gemini API Key
GEMINI_API_KEY = "API_KEY_HERE"
GEMINI_ENDPOINT = f"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={GEMINI_API_KEY}"

PRE_PROMPT = (
    "You are a helpful banking application FAQ assistant. "
    "Always respond clearly in 3 to 5 bullet points only, no more. "
    "Keep responses short and specific to what users would ask in a banking app (like creating an account, applying for loans, etc.)."
)

@app.route('/generate', methods=['POST'])
def generate_response():
    data = request.get_json()
    user_prompt = data.get('prompt')

    if not user_prompt:
        return jsonify({'error': 'No prompt provided'}), 400

    headers = {
        'Content-Type': 'application/json'
    }

    full_prompt = f"{PRE_PROMPT}\n\nUser question: {user_prompt}"

    payload = {
        "contents": [
            {
                "parts": [
                    {
                        "text": full_prompt
                    }
                ]
            }
        ]
    }

    try:
        response = requests.post(GEMINI_ENDPOINT, headers=headers, json=payload)
        response.raise_for_status()
        response_data = response.json()

        # Extract the generated text from the response
        text = response_data['candidates'][0]['content']['parts'][0]['text']
        return jsonify({'generated_text': text})
    except requests.exceptions.RequestException as e:
        return jsonify({'error': str(e)}), 500
    except (KeyError, IndexError) as e:
        return jsonify({'error': 'Unexpected response format', 'details': str(e)}), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=9001)
