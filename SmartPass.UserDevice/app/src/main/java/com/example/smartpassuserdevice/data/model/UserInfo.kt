package com.example.smartpassuserdevice.data.model

import java.util.UUID

data class UserInfo (
    val id: UUID,
    val userName: String,
    val department: String,
)