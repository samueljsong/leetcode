/**
 * Given an integer array nums, return true if any value appears at least twice in the array, and return false if every element is distinct.
 *
 * @param {number[]} nums
 * @return {boolean}
 */
var containsDuplicate = function (nums) {
    const hashMap = new Map();

    for (let i = 0; i < nums.length; i++) {
        hashMap[nums[i]] = (hashMap[nums[i]] ?? 0) + 1;
        if (hashMap[nums[i]] >= 2) {
            return true;
        }
    }
    return false;
};

/**
 *
 * An Anagram is a word or phrase formed by rearranging the letters of a different word or phrase, typically using all the original letters exactly once.
 * @param {string} s
 * @param {string} t
 * @return {boolean}
 */
var isAnagram = function (s, t) {
    let tempS = s.split("").sort();
    let tempT = t.split("").sort();

    return JSON.stringify(tempS) === JSON.stringify(tempT);
};
