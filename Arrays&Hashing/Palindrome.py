import re

class Solution:
    def isPalindrome(self, s: str) -> bool:

        word = re.sub(r'[^a-zA-Z]', '', s).lower()

        if len(word) == 2:
            if word[0] != word[1]:
                return False

        word_len = len(word)

        for i in range(word_len // 2):
            if word[i] != word[word_len - 1 - i]:
                return False
        return True

solution = Solution()
print(solution.isPalindrome('0P'))
# print(Solution.isPalindrome('OP'))