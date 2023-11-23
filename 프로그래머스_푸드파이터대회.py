def solution(food):
    ans = ''
    f_lst = []
    for i in range(1, len(food)):
        cnt = food[i]//2
        for j in range(cnt):
            f_lst.append(str(i))
    ans = ''.join(f_lst)
    ans1 = ''.join("0")
    ans2 = ''.join(f_lst[::-1])
    
    return ans+ans1+ans2
