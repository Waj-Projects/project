# 5.Design and Implementation of an Information Retrieval System with Indexing, Stop-word Removal, and Stemming in Python.
from nltk.corpus import stopwords
from nltk.stem import PorterStemmer
from collections import defaultdict
import re


class InformationRetrivalSystem:
    def __init__(self, documents):
        self.documents = documents
        self.stemmer = PorterStemmer()
        self.stopwords = stopwords.words("english")
        self.index = defaultdict(list[str])
        self.build_index()

    def preprocess(self, text):
        tokens = re.findall(r"\b\w+\b", text.lower())
        return [
            self.stemmer.stem(token) for token in tokens if token not in self.stopwords
        ]

    def build_index(self):
        for doc_id, doc_text in enumerate(self.documents):
            unique_tokens = set(self.preprocess(doc_text))
            for token in unique_tokens:
                self.index[token].append(doc_id)

    def search(self, query):
        tokens = self.preprocess(query)
        if not tokens:
            return []
        unique_result_docs = set(self.index.get(tokens[0], []))
        for token in tokens[1:]:
            unique_result_docs &= set(self.index.get(token, []))
        return list(unique_result_docs)


if __name__ == "__main__":
    docs = [
        "Python is a programming language. Python os popular for data science.",
        "Information retrieval is a field of computer science.",
        "Stemming helps in information retrieval by reducing words to their root forms",
        "Stop-word removal improves the efficiency of indexing and searching.",
    ]

    ir_system = InformationRetrivalSystem(docs)
    query1 = "Python programming"
    query2 = "Information retrieval"

    print(query1, "->", ir_system.search(query1))
    print(query2, "->", ir_system.search(query2))
