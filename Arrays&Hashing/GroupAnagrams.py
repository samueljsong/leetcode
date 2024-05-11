
# Given an array of strings strs, group the anagrams together. 
# You can return the answer in any order.

# An Anagram is a word or phrase formed by rearranging the letters 
# of a different word or phrase, typically using all the original letters exactly once.


class Solution:
    def groupAnagrams(self, strs: List[str]) -> List[List[str]]:
        my_dict = {}
        for word in strs:
            sorted_word = ''.join(sorted(word))
            if sorted_word not in my_dict:
                my_dict[sorted_word] = []

        for word in strs:
            sorted_str = ''.join(sorted(word))
            my_dict[sorted_str].append(word)
        
        return(list(my_dict.values()))

        
