# 1.Positional Encoding-Implement a python code to do positional encoding in GPT

import torch
import math


# positional encoding
def positional_encoding(seq_len, d_model):
    pos_enc = torch.zeros(seq_len, d_model)
    for pos in range(seq_len):
        for i in range(0, d_model, 2):
            pos_enc[pos, i] = math.sin(pos / (10000 ** (i / d_model)))
            if i + 1 < d_model:
                pos_enc[pos, i + 1] = math.cos(pos / (10000 ** (i / d_model)))
    return pos_enc


# Example token embeddings (pretend these came from an embedding layer)
EMB = {
    "The": torch.tensor([0.2, 0.5, 0.1, 0.7]),
    "cat": torch.tensor([0.3, 0.6, 0.8, 0.1]),
    "sleeps": torch.tensor([0.4, 0.9, 0.2, 0.5]),
}


def encode_sentence(tokens):
    X = torch.stack([EMB[t] for t in tokens])
    P = positional_encoding(seq_len=len(tokens), d_model=X.size(1))
    E = X + P
    return X, P, E


# ---- Sentence A: "The cat sleeps"
tokens_A = ["The", "cat", "sleeps"]
X_A, P_A, E_A = encode_sentence(tokens_A)

# ---- Sentence B: "sleeps cat The" (same words, different order)
tokens_B = ["sleeps", "The", "cat"]
X_B, P_B, E_B = encode_sentence(tokens_B)

# ---- Inspect
print("Tokens A:", tokens_A)
print("query tokens in order:\n", E_A)

print("\nTokens B:", tokens_B)
print("order changed:\n", E_B)
