package com.example.smartpassuserdevice.data.model

import java.util.UUID

data class UserOwner (
    val userId: UUID,
    val userName: String,
    val cardsSynchronized: Boolean,
)