package com.ssafy.fcmus.entity.live;

import com.ssafy.fcmus.entity.member.Member;
import lombok.Getter;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Getter
@NoArgsConstructor
@Entity
@Table(name = "live_like")
public class LiveLike {
    //필드
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "live_like_id")
    private Long id;

    // 연결
    @ManyToOne(cascade = CascadeType.MERGE, targetEntity = Member.class)
    @JoinColumn(name = "member_id", updatable = false)
    private Member member;

    @ManyToOne(cascade = CascadeType.MERGE, targetEntity = Live.class)
    @JoinColumn(name = "live_id", updatable = false)
    private Live live;



}

