package com.example.smartpassuserdevice.data.model

data class GetMyAccessCardsMobileResponse (
    val user : UserOwner,
    val accessCards: List<AccessCard>
)