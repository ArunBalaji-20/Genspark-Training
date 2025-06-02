from flask import Flask, request, jsonify
from transformers import AutoTokenizer, AutoModelForCausalLM, AutoConfig
import torch

app = Flask(__name__)

print("Starting to load the tokenizer and model...")

tokenizer = AutoTokenizer.from_pretrained("microsoft/phi-2")

# Ensure the tokenizer has a pad token
if tokenizer.pad_token is None:
    tokenizer.add_special_tokens({'pad_token': '[PAD]'})

config = AutoConfig.from_pretrained("microsoft/phi-2")
model = AutoModelForCausalLM.from_pretrained("microsoft/phi-2", config=config)

print("Model and tokenizer loaded successfully.")

# Define the pre-prompt
PRE_PROMPT = (
    "Act as a helpful banking app FAQ assistant. Answer clearly and concisely in 3 to 5 bullet points max.\n\n"
    "User question:\n"
)

@app.route('/generate', methods=['POST'])
def generate_text():
    if request.method == 'POST':
        data = request.json
        user_prompt = data.get('prompt', '')

        if user_prompt:
            # Combine pre-prompt and user input
            full_prompt = f"{PRE_PROMPT} {user_prompt}\n\nAnswer:"

            input_ids = tokenizer.encode(full_prompt, return_tensors='pt', padding='max_length', truncation=True, max_length=512)
            attention_mask = input_ids.ne(tokenizer.pad_token_id).int()

            output = model.generate(
                input_ids=input_ids,
                attention_mask=attention_mask,
                num_return_sequences=1,
                pad_token_id=tokenizer.eos_token_id,
                temperature=0.7,
                top_k=50,
                top_p=0.95,
                do_sample=True,
                max_new_tokens=100
            )

            generated_text = tokenizer.decode(output[0], skip_special_tokens=True)
            response_text = generated_text.split("Answer:")[-1].strip()

            return jsonify({'generated_text': response_text})
        else:
            return jsonify({'error': 'No prompt provided'}), 400

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=9001)



