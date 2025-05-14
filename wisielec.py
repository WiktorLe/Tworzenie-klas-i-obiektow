import random

class WordBank:
    def __init__(self):
        self.words = ["python", "programowanie", "wisielec", "komputer", "klasa"]

    def get_random_word(self):
        return random.choice(self.words)


class Player:
    def __init__(self):
        self.guessed_letters = set()

    def guess(self):
        letter = input("Podaj literę: ").lower()
        return letter


class Game:
    def __init__(self):
        self.word_bank = WordBank()
        self.player = Player()
        self.word = self.word_bank.get_random_word()
        self.max_errors = 7
        self.errors = 0
        self.correct_letters = set()

    def display_current_state(self):
        displayed = [letter if letter in self.correct_letters else "_" for letter in self.word]
        print(" ".join(displayed))
        print(f"Błędy: {self.errors}/{self.max_errors}")
        print(f"Odgadnięte litery: {' '.join(sorted(self.player.guessed_letters))}")

    def check_game_status(self):
        if self.errors >= self.max_errors:
            print(f"Przegrałeś! Słowo to: {self.word}")
            return True
        elif set(self.word).issubset(self.correct_letters):
            print(f"Gratulacje! Odgadłeś słowo: {self.word}")
            return True
        return False

    def play(self):
        print("=== Gra w Wisielca ===")
        while True:
            self.display_current_state()
            letter = self.player.guess()

            if letter in self.player.guessed_letters:
                print("Już próbowałeś tej litery.")
                continue

            self.player.guessed_letters.add(letter)

            if letter in self.word:
                self.correct_letters.add(letter)
                print("Dobrze!")
            else:
                self.errors += 1
                print("Źle!")

            if self.check_game_status():
                break

        print("Koniec gry.")

gra = Game()
gra.play()
