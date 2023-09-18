def solution(sizes):
    ans = 0
    card = []
    h, v = [], []
    for c in sizes:
        h.append(min(c))
        v.append(max(c))
        
    return max(h)*max(v)