import nltk
from nltk.corpus import wordnet as wn
from nltk.tokenize import word_tokenize

nltk.download("wordnet")
nltk.download("punkt")
nltk.download("omw-1.4")


def simple_lesk(word, sentence):
    # Step 1: Get words from sentence
    sentence_words = set(word_tokenize(sentence.lower()))

    best_sense = None
    max_overlap = 0

    # Step 2: Check each meaning of the word
    for sense in wn.synsets(word):
        # Step 3: Get definition words
        definition_words = set(word_tokenize(sense.definition().lower()))
        for ex in sense.examples():
            definition_words |= set(word_tokenize(ex.lower()))
        # Step 4: Count matching words
        overlap = 0
        for word in sentence_words:
            if word in definition_words:
                overlap += 1

        # Step 5: Keep the best match
        if overlap > max_overlap:
            max_overlap = overlap
            best_sense = sense

    return best_sense


# Example 1
sentence1 = "He deposited the cash at the bank near the river after fishing."
result1 = simple_lesk("bank", sentence1)
print("Sense:", result1.name())
print("Definition:", result1.definition())

# Example 2
sentence2 = "The bat flew silently across the night sky in search of insects."
result2 = simple_lesk("bat", sentence2)
print("Sense:", result2.name())
print("Definition:", result2.definition())
