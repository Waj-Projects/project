import re
from fastcoref import FCoref

text = "Alice met Rob after the office. He told her the report was ready and she thanked him."

# Load model and predict
model = FCoref()
result = model.predict(texts=[text])[0]

# Print clusters
print("Clusters:")
clusters = result.get_clusters(as_strings=True)
for cluster in clusters:
    print(cluster)

# Resolve pronouns
resolved = text
for cluster in clusters:
    # Pick the longest capitalized word as representative
    representative = max(cluster, key=len)
    print("Representative:", representative)

    # Replace all other words with representative
    for word in cluster:
        if word != representative:
            resolved = re.sub(
                r"\b" + word + r"\b", representative, resolved, flags=re.IGNORECASE
            )

print("\nResolved text:\n", resolved)
