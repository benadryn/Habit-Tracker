using Firebase.Auth;
using TMPro;
using UnityEngine;

public class SignUpNewUser : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TMP_InputField confirmPasswordField;
    private FirebaseAuth auth;
    public State CurrentState { get; private set; }
    // public StateChangeEvent OnStateChanged = new StateChangedEvent();

    public string Email => emailField.text;
    public string Password => passwordField.text;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        
        emailField.onValueChanged.AddListener(HandleValueChanged);
        passwordField.onValueChanged.AddListener(HandleValueChanged);
        confirmPasswordField.onValueChanged.AddListener(HandleValueChanged);
        ComputeState();
    }

    private void HandleValueChanged(string _)
    {
        ComputeState();
    }

    private void ComputeState()
    {
        if (string.IsNullOrEmpty(emailField.text))
        {
            SetState(State.EnterEmail);
        }
        else if (string.IsNullOrEmpty(passwordField.text))
        {
            SetState(State.EnterPassword);
        }
        else if (passwordField.text != confirmPasswordField.text)
        {
            SetState(State.PasswordsDontMatch);
        }
        else
        {
            SetState(State.Ok);
        }
    }

    private void SetState(State state)
    {
        CurrentState = state;
        // OnStateChanged.Invoke(state);
    }
    

    public void OnSignUpButtonClick()
    {
        emailField.image.color = Color.white;
        emailField.image.color = Color.white;
        confirmPasswordField.image.color = Color.white;
        if (CurrentState == State.Ok)
        {
            CreateAccount();
        }

        if (CurrentState == State.EnterEmail)
        {
            emailField.image.color = Color.red;
        }

        if (CurrentState == State.EnterPassword)
        {
            passwordField.image.color = Color.red;
        }
        if (CurrentState == State.PasswordsDontMatch)
        {
            confirmPasswordField.image.color = Color.red;
        }
    }

    private void CreateAccount()
    {
        if (!IsValidEmail(Email))
        {
            CurrentState = State.EnterEmail;
            return;
        }
        auth.CreateUserWithEmailAndPasswordAsync(Email, Password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    
    public enum State
    {
        EnterEmail,
        EnterPassword,
        PasswordsDontMatch,
        Ok
    }
}