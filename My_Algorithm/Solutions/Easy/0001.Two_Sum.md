
# 문제

### Two-Sum
 - https://leetcode.com/problems/two-sum/
 - 입력받은 정수 배열 중 두 수의 합이 타겟과 일치하는 쌍을 찾는 문제이다.

# 해결 방법

### Brute-force
 - 버블 정렬처럼 모든 조합을 탐색한다.
```
class Solution {
public:
    vector<int> twoSum(vector<int>& nums, int target) {
        vector<int> res;
        for (int i = 0; i < nums.size() - 1; i++)
        {
            for (int j = i + 1; j < nums.size(); j++)
            {
                if (nums[i] + nums[j] == target)
                {
                    res.push_back(i);
                    res.push_back(j);
                    return (res);
                }
            }
        }
        return (res);
    }
};
```

### HashTable
 - 해시 테이블의 key를 정수값으로, value를 해당 값에 대한 입력배열의 위치로 입력한다.
   - 이 방법이 가능한 이유는 입력값에 중복이 없기 때문에 해시의 키값으로 할당이 가능하기 때문이다.
 - 값을 하나씩 보며 목표값과 확인하고 있는 값을 뺀 값이 해시키에 존재하면 결과를 반환하고, 아니면 새로운 값을 해시테이블에 추가한다.
```
class Solution {
public:
    vector<int> twoSum(vector<int>& nums, int target) {
        vector<int> ans;
        unordered_map <int, int> m;
        for (int i = 0; i < nums.size(); i++){
            if (m.find(target-nums[i]) != m.end()){
                ans.push_back(i);
                ans.push_back(m[target-nums[i]]);
                return ans;
            }
            m[nums[i]] = i;
        }
        return {};
    }
};
```
