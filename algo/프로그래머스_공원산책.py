def solution(park, routes):
    ans = []
    in_x = 0  # 변수명 수정
    routes_dt = {'E': [0, 1], 'W': [0, -1], 'S': [1, 0], 'N': [-1, 0]}
    N = len(park)
    M = len(park[0])
    
    for r in range(N):
        for c in range(M):
            if park[r][c] == 'S':
                ans.append(r)
                ans.append(c)
                
    for route in routes:  # 수정: routes를 순회하면서 op와 n을 구함
        op, n = route.split(' ')
        n = int(n)  # 문자열을 정수로 변환
        
        go_r = routes_dt[op][0]
        go_c = routes_dt[op][1]
        
        if 0 <= ans[0] + go_r * n < N and 0 <= ans[1] + go_c * n < M:  # 범위 조건 수정
            if op == 'E':
                if 'X' not in park[ans[0]][ans[1]: ans[1] + go_c * n + 1]:  # 문자열 검사 로직 수정
                    ans[0] += go_r
                    ans[1] += go_c * n
                    print(f'{ans} 상')
            if op == 'W':
                if 'X' not in park[ans[0]][ans[1] + go_c * n: ans[1]]:  # 문자열 검사 로직 수정
                    ans[0] += go_r
                    ans[1] += go_c * n
                    print(f'{ans} 상')
            if op == 'S':
                in_x = 0  # 변수명 수정
                for j in range(ans[0], ans[0] + go_r * n + 1):
                    if park[j][ans[1]] == 'X':
                        in_x = 1
                if in_x == 0:
                    ans[0] += go_r * n
                    ans[1] += go_c
                    print(f'{ans} 하')
            if op == 'N':
                in_x = 0  # 변수명 수정
                for j in range(ans[0], ans[0] + go_r*n-1, -1):
                    if park[j][ans[1]] == 'X':
                        in_x = 1
                if in_x == 0:
                    ans[0] += go_r * n
                    ans[1] += go_c
                    print(f'{ans} 하')
                    
    return ans
