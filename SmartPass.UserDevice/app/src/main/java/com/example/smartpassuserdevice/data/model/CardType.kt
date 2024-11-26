package com.example.smartpassuserdevice.data.model

enum class CardType(val code: Int) {
    None(0),
    Fizic(1),
    Digital(2);

    companion object {
        fun fromCode(code: Int): CardType {
            return values().find { it.code == code } ?: throw IllegalArgumentException("Invalid code for CardType: $code")
        }
    }
}