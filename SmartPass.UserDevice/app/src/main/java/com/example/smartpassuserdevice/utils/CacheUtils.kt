import android.content.Context
import android.content.SharedPreferences
import com.example.smartpassuserdevice.data.model.LoginResponse
import com.example.smartpassuserdevice.data.model.GetMyAccessCardsMobileRsp
import com.example.smartpassuserdevice.data.model.UserInfo
import com.google.gson.Gson
import com.auth0.android.jwt.JWT
import com.example.smartpassuserdevice.data.model.AccessCard
import java.util.*

object CacheUtils {

    // SharedPreferences keys
    private const val PREF_NAME = "auth_data"
    private const val KEY_TOKEN = "token"
    private const val KEY_REFRESH_TOKEN = "refresh_token"
    private const val KEY_USER_INFO = "user_info"
    private const val KEY_USER_CARDS = "user_cards"
    private const val KEY_SELECTED_CARD_INDEX = "selected_card_index"
    private const val KEY_SELECTED_CARD = "selected_card"

    // Helper function to get SharedPreferences
    private fun getSharedPreferences(context: Context): SharedPreferences {
        return context.getSharedPreferences(PREF_NAME, Context.MODE_PRIVATE)
    }

    // Save login response to cache
    fun saveLoginResponseToCache(context: Context, loginResponse: LoginResponse) {
        val sharedPreferences = getSharedPreferences(context)

        // Decode JWT token to extract user info
        val jwtToken = JWT(loginResponse.token)
        val refreshToken = loginResponse.refreshToken

        val idString = jwtToken.getClaim("Id").asString()
        val userName = jwtToken.getClaim("UserName").asString() ?: ""
        val department = jwtToken.getClaim("Department").asString() ?: ""

        val id = idString?.let { UUID.fromString(it) } ?: UUID.randomUUID()

        // Create UserInfo object
        val userInfo = UserInfo(
            id = id,
            userName = userName,
            department = department
            )

        // Save data to SharedPreferences
        with(sharedPreferences.edit()) {
            putString(KEY_TOKEN, loginResponse.token)
            putString(KEY_REFRESH_TOKEN, refreshToken.toString())
            putString(KEY_USER_INFO, Gson().toJson(userInfo))
            apply()
        }
    }

    // Save user cards response to cache
    fun saveUserCardsResponseToCache(context: Context, myCardsResponse: GetMyAccessCardsMobileRsp) {
        val sharedPreferences = getSharedPreferences(context)

        // Save user cards to SharedPreferences
        with(sharedPreferences.edit()) {
            putString(KEY_USER_CARDS, Gson().toJson(myCardsResponse))
            apply()
        }
    }

    // Get user info from cache
    fun getUserInfoFromCache(context: Context): UserInfo? {
        val sharedPreferences = getSharedPreferences(context)
        val userInfoJson = sharedPreferences.getString(KEY_USER_INFO, null)
        return if (userInfoJson != null) {
            Gson().fromJson(userInfoJson, UserInfo::class.java)
        } else {
            null
        }
    }

    // Get user cards from cache
    fun getUserCardsFromCache(context: Context): GetMyAccessCardsMobileRsp? {
        val sharedPreferences = getSharedPreferences(context)
        val userCardsJson = sharedPreferences.getString(KEY_USER_CARDS, null)
        return if (userCardsJson != null) {
            Gson().fromJson(userCardsJson, GetMyAccessCardsMobileRsp::class.java)
        } else {
            null
        }
    }

    // Get token from cache
    fun getTokenFromCache(context: Context): String? {
        val sharedPreferences = getSharedPreferences(context)
        return sharedPreferences.getString(KEY_TOKEN, null)
    }

    // Get refresh token from cache
    fun getRefreshTokenDataFromCache(context: Context): String? {
        val sharedPreferences = getSharedPreferences(context)
        return sharedPreferences.getString(KEY_REFRESH_TOKEN, null)
    }

    // Save selected card index to cache
    fun saveSelectedCardIndexToCache(context: Context, selectedIndex: Int) {
        val sharedPreferences = getSharedPreferences(context)
        with(sharedPreferences.edit()) {
            putInt(KEY_SELECTED_CARD_INDEX, selectedIndex)
            apply()
        }
    }

    // Get selected card index from cache
    fun getSelectedCardIndexFromCache(context: Context): Int {
        val sharedPreferences = getSharedPreferences(context)
        return sharedPreferences.getInt(KEY_SELECTED_CARD_INDEX, 0)
    }

    // Save selected card to cache
    fun saveSelectedCardToCache(context: Context, selectedCard: AccessCard) {
        val sharedPreferences = getSharedPreferences(context)
        with(sharedPreferences.edit()) {
            putString(KEY_SELECTED_CARD, Gson().toJson(selectedCard))
            apply()
        }
    }

    // Get selected card from cache
    fun getSelectedCardFromCache(context: Context): AccessCard? {
        val sharedPreferences = getSharedPreferences(context)
        val selectedCardJson = sharedPreferences.getString(KEY_SELECTED_CARD, null)
        return if (selectedCardJson != null) {
            Gson().fromJson(selectedCardJson, AccessCard::class.java)
        } else {
            null
        }
    }

    // Clear all cached data
    fun clearCache(context: Context) {
        val sharedPreferences = getSharedPreferences(context)
        with(sharedPreferences.edit()) {
            // Remove all saved data
            remove(KEY_TOKEN)
            remove(KEY_REFRESH_TOKEN)
            remove(KEY_USER_INFO)
            remove(KEY_USER_CARDS)
            putInt(KEY_SELECTED_CARD_INDEX, 0)
            apply()
        }
    }
}
