package com.example.smartpassuserdevice.data.model

enum class CardState(val code: Int) {
    None(0),
    Blocked(1),
    NotAssigned(2),
    Active(3);

    companion object {
        fun fromCode(code: Int): CardState {
            return CardState.values().find { it.code == code } ?: throw IllegalArgumentException("Invalid code for CardState: $code")
        }
    }
}
