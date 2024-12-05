package com.example.smartpassuserdevice.data.model

data class GetMyAccessCardsMobileRsp (
    val userInfo : UserInfo,
    val accessCards: List<AccessCard>
)