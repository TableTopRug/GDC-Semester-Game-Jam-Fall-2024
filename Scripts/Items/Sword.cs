using Godot;


public partial class Sword : Weapon
{
    private float RotateTo;


    public override float Attack()
    {
        RotateTo = 120;

        return 0f;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (RotateTo != 0.0f) 
        {
            float rotation = Rotation;
            
            if (Mathf.Abs(rotation - RotateTo) >= .02f)
            {
                rotation = (float)Mathf.LerpAngle(rotation, RotateTo, delta);

                Rotation = rotation;
            } else {
                RotateTo = 0.0f;
            }
        }

        base._PhysicsProcess(delta);
    }
}