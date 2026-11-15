using Godot;
using System;

[GlobalClass]
public partial class Player : CharacterBody3D
{
    private float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    [Export]
    private float Speed = 5.0f;
    [Export]
    private float MouseSensitivity = 0.002f;
    [Export]
    private Godot.Collections.Array<Node> Inventory = new Godot.Collections.Array<Node>();
    private IInteractable HoldingObject;
    private bool ControlBlock = false;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        GetNode<Button>("CanvasLayer/ReadMenu/close").Pressed += CloseReadable; // добавляет функионал кнопки "закрыть" в тексте книги или заметки
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;
        velocity.Y -= gravity * (float)delta;

        Vector2 input = Input.GetVector("left", "right", "forward", "backward");
        Vector3 movementDirection = Transform.Basis * new Vector3(input.X, 0, input.Y);
        velocity.X = movementDirection.X * Speed;
        velocity.Z = movementDirection.Z * Speed;
        
        Velocity = velocity;
        if (!ControlBlock)
        {
            MoveAndSlide();
        }   
    }

    public void AddToInventory(Node NodeToAdd) // функция добавления предмета в инвентарь и изменения счётчика поднятых предметов
    {
        Inventory.Add(NodeToAdd);
        GetNode<Label>("CanvasLayer/LabelItemsNumber").Text = Inventory.Count.ToString();  
    }

    public override void _Process(double delta)
    {
        Label label = GetNode<Label>("CanvasLayer/CameraLabel");
        RayCast3D rayCast = GetNode<RayCast3D>("Camera3D/RayCast3D");
        rayCast.ForceRaycastUpdate();

        label.Text = rayCast.GetCollider() is IInteractable interactableCollider ? interactableCollider.GetTooltipText : ""; // задает текст подсказки при наведении на объект, с которым можно провзаимодействовать
        if (rayCast.GetCollider() is IInteractable interactable && Input.IsActionJustPressed("interact")) // если у объекта есть интерфейс IInteractable, то с ним можно провзаимодействовать
        {
            interactable.Interact(this) ;
        }

        if (rayCast.GetCollider() is IDamageable damagable && Input.IsActionJustPressed("attack")) // если у объекта есть интерфейс IDamagable, то его можно уничтожить
        {
            damagable.ReceiveDamage(5) ;
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion && !ControlBlock)
        {
            RotateY(-mouseMotion.Relative.X * MouseSensitivity);

            Camera3D camera = GetNode<Camera3D>("Camera3D");
            camera.RotateX(-mouseMotion.Relative.Y * MouseSensitivity);
            Vector3 cameraRotation = camera.Rotation;
            cameraRotation.X = Mathf.Clamp(cameraRotation.X, Mathf.DegToRad(-70), Mathf.DegToRad(70));
            camera.Rotation = cameraRotation;
        }
        if (@event.IsActionPressed("quit"))
        {
            GetTree().Quit();
        }
    }


    public void ToggleREading(IReadable Readable) // открывает менюшку чтения книги или заметки и блокирует управление
    {
        GetNode<Label>("CanvasLayer/ReadMenu/PanelContainer/MarginContainer/ScrollContainer/Label").Text = Readable.Text;
        GetNode<Control>("CanvasLayer/ReadMenu").Visible = !GetNode<Control>("CanvasLayer/ReadMenu").Visible;
        HoldingObject = Readable;
        ControlBlock = GetNode<Control>("CanvasLayer/ReadMenu").Visible;
        Input.MouseMode = GetNode<Control>("CanvasLayer/ReadMenu").Visible? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured;
    }

    public void CloseReadable () 
    {
        
        if (HoldingObject is IReadable readableObject) 
        {
        readableObject.Close(this);
        }
    }
}
