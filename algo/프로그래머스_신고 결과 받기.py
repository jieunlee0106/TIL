def solution(id_list, report, k):
    answer = []
    dic = {}
    cnt_dic = {}

    for idx in id_list:
        dic[idx] = []
        cnt_dic[idx] = 0

    # 계정 별 신고 횟수 누적
    for i in report:
        p1, p2 = i.split()
        if p2 not in dic[p1]:
            dic[p1].append(p2)
            cnt_dic[p2] += 1
    
    # 정지 계정 
    lst = []
    for i in range(len(id_list)):
        if cnt_dic[id_list[i]] >= k:
            lst.append(id_list[i])
    
    
    for i in range(len(id_list)):
        cnt = 0
        for j in range(len(dic[id_list[i]])):
            if dic[id_list[i]][j] in lst:
                cnt += 1
        answer.append(cnt)
    return answer



# 타 풀이

def solution(id_list, report, k):
    answer = [0] * len(id_list)    
    reports = {x : 0 for x in id_list}

    for r in set(report):
        reports[r.split()[1]] += 1

    for r in set(report):
        if reports[r.split()[1]] >= k:
            answer[id_list.index(r.split()[0])] += 1

    return answer
