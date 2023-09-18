
def solution(book_time):
    ans = 0
    time = [ 0 for _ in range(1452)]
    for i in book_time:
        s, e = i
        h, m = s.split(':')
        st = int(h)*60 + int(m) + 1

        h, m = e.split(':')
        et = int(h)*60 + int(m) + 10
        
        for j in range(st, et+1):
            time[j] += 1
    return max(time)
