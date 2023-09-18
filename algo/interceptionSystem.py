
s = "aukks"
skip = "wbqd"
iii = 5

def solution(s, skip, iii):
    answer = ''
    al = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'n', 'm', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
          'w', 'x', 'y', 'z']

    for w in skip:
        al.remove(w)

    N = len(al)

    for w in s:
        windex = al.index(w)
        windex += iii
        windex = windex % N
        answer += al[windex]

    return answer

print(solution(s,skip,iii))