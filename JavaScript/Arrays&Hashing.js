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

/**
 * Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.
 * @param {number[]} nums
 * @param {number} target
 * @return {number[]}
 */
var twoSum = function (nums, target) {
    let hashMap = new Map();

    for (let i = 0; i < nums.length; i++) {
        //iterates through an array
        if (hashMap.has(target - nums[i])) {
            //checks if map has the number = target - nums[i]
            return [hashMap.get(target - nums[i]), i]; // if it does that means that targets index and the current index is the pair
        } else {
            hashMap.set(nums[i], i); //if not then add the number as the key and index as the value
        }
    }
    return []; //if there is nothing, return nothing
};

/**
 * Given an array of strings strs, group the anagrams together. You can return the answer in any order.
 * @param {string[]} strs
 * @return {string[][]}
 */
var groupAnagrams = function (strs) {
    let map = new Map();

    strs.forEach((str) => {
        let splitString = str.split("").sort().join("");
        if (map.has(splitString)) {
            map.get(splitString).push(str);
        } else {
            map.set(splitString, [str]);
        }
    });

    return Array.from(map.values());
};

/**
 * Given an integer array nums and an integer k, return the k most frequent elements. You may return the answer in any order.
 * @param {number[]} nums
 * @param {number} k
 * @return {number[]}
 */
var topKFrequent = function (nums, k) {
    let map = new Map();

    nums.forEach((num) => {
        if (map.has(num)) {
            map.set(num, map.get(num) + 1);
        } else {
            map.set(num, 1);
        }
    });
    let sortedEntries = Array.from(map.entries()).sort((a, b) => b[1] - a[1]);

    let res = [];
    for (let i = 0; i < k; i++) {
        res.push(sortedEntries[i][0]);
    }
    return res;
};

/**
 * Given an integer array nums, return an array answer such that answer[i] is equal to the product of all the elements of nums except nums[i].
 * @param {number[]} nums
 * @return {number[]}
 */
var productExceptSelfVersion1 = function (nums) {
    let res = [];
    for (let i = 0; i < nums.length; i++) {
        let val = nums.reduce((total, num) => {
            if (num === nums[i]) {
                return total;
            }
            return total * num;
        });
        res.push(val);
    }
    return res;
};

/**
 * Given an integer array nums, return an array answer such that answer[i] is equal to the product of all the elements of nums except nums[i].
 * @param {number[]} nums
 * @return {number[]}
 */
var productExceptSelf = function (nums) {
    let result = [];

    let left = 1;
    let right = 1;

    for (let i = 0; i < nums.length; i++) {
        result[i] = left;
        left *= nums[i];
    }

    for (let i = nums.length - 1; i >= 0; i--) {
        result[i] *= right;
        right *= nums[i];
    }

    return result;
};
