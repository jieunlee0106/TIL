def solution(X, Y):
    answer = ''
    checked = []
    for i in range(len(X)):
        if X[i] in Y and X[i] not in checked:
            cnt = min(Y.count(X[i]), X.count(X[i]))
            checked.append(X[i])
            for j in range(cnt):
                answer += X[i]
                
    if answer == '':
        return "-1"
    if checked == ["0"]:
        return "0"
    return "".join(sorted(answer, reverse=True))
