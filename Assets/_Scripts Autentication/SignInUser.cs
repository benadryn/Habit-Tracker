using System;
using Firebase.Auth;
using TMPro;
using UnityEngine;

public class SignInUser : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailField;
    [SerializeField] private TMP_InputField passwordField;
    private FirebaseAuth auth;

    public string Email => emailField.text;
    public string Password => passwordField.text;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

    }

    public void SignIn()
    {
        auth.SignInWithEmailAndPasswordAsync(Email, Password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }
}