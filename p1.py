from sklearn.feature_extraction.text import TfidfVectorizer

# Documents
d0 = "I love machine learning"
d1 = "I love arrtificial intelligence"
d2 = "We love NLP"

documents = [d0, d1, d2]

# Create TF-IDF vectorizer
tfidf = TfidfVectorizer()
tfidf_matrix = tfidf.fit_transform(documents).toarray()

words = tfidf.get_feature_names_out()
idf_values = tfidf.idf_

tf_matrix = tfidf_matrix / idf_values

print("TF VALUES:")
for i in range(len(documents)):
    print(f"DOC{i} -> {dict(zip(words, tf_matrix[i]))}")

print("IDF VALUES:")
for i in range(len(words)):
    print(f"{words[i]} : {idf_values[i]}")
