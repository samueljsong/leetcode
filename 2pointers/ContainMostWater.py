class Solution:
    def maxArea(self, height: List[int]) -> int:
        p1 = 0
        p2 = len(height) - 1
        area = []
        for _ in range(len(height)):
            
            area.append(min(height[p1], height[p2]) * (p2 - p1))

            if height[p1] > height[p2]:
                p2 -= 1
            else:
                p1 +=1
        
        return max(area)
        