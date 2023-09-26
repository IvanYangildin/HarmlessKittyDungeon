using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float sensitivity;

    PlayerIA inputActions;
    PlayerIA.MoveMapActions moveActions;
    PlayerIA.InteractionsActions interactionsActions;
    PlayerIA.UIActions uiActions;

    [SerializeField]
    UnitMove playerMove;
    [SerializeField]
    PlayerLook playerLook;
    [SerializeField]
    PlayerInteract playerInteract;
    [SerializeField]
    PlayerAttack playerAttack;

    [SerializeField]
    PauseGame pauseGame;


    private void Awake()
    {
        inputActions = new PlayerIA();
        moveActions = inputActions.MoveMap;
        interactionsActions = inputActions.Interactions;
        uiActions = inputActions.UI;

        interactionsActions.Interact.performed += ctx => playerInteract.Interact();
        interactionsActions.Hit.performed += ctx => playerAttack.Attack();
        interactionsActions.Hit.canceled += ctx => playerAttack.StopAttack();

        interactionsActions.NextWeapon.performed += ctx => playerAttack.ChangeWeapon(
            interactionsActions.NextWeapon.ReadValue<float>()
        );

        uiActions.PauseMenu.performed += ctx =>
        {
            pauseGame.Switch();
            if (pauseGame.IsPaused)
            {
                moveActions.Disable();
                interactionsActions.Disable();
            }
            else
            {
                moveActions.Enable();
                interactionsActions.Enable();
            }
        };
    }

    private void FixedUpdate()
    {
        playerMove.MoveLocal(moveActions.Move.ReadValue<Vector2>());
        playerAttack.ChoseWeapon(
            Mathf.CeilToInt(interactionsActions.ChooseWeapon.ReadValue<float>() - 1));

    }

    private void LateUpdate()
    {
        playerLook.Look(moveActions.Look.ReadValue<Vector2>(), sensitivity);
    }

    private void OnEnable()
    {
        moveActions.Enable();
        interactionsActions.Enable();
        uiActions.Enable();
    }

    private void OnDisable()
    {
        moveActions.Disable();
        interactionsActions.Disable();
        uiActions.Disable();
    }
}