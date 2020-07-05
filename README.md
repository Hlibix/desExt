# Description

**desExt** is a Unity framework providing flexible control over game variables and other extended design capabilities using **ScriptableObjects**

# Features

## Variables
**Variable** in desExt is an asset representing one of the basic data types (`float`, `int`, `string`, `GameObject`, `Vector2/3/4`, etc.)

**Variable** can be created through either through **Project Window** or **Assets Menu** via selecting `Create -> desExt -> Variables -> X Variable`

After, a new instance of the **Variable** is created and is ready to be setup in inspector

Later, an instance of the **Variable** can be used either independently or as a part of **Reference**

### Variables Manager

A list of all **Variables** is available for editing in `desExt -> Variables Manager` menu

## References
**Reference** is a special data type allowing user to reference a **Variable** or the **constant value** of specified type. It allows for reusing of one value for multiple instances of objects where this value is used

**Constant** and **Variable** values can be toggle by clicking the checkbox in inspector where the **Reference** is declared

**Reference** is used in code as a serialized field and can replace ordinary variable (`except for setting the value or using it as string`) with just replacing the field type to according **Reference**

### Was
``` csharp
float Damage;

void DealDamage()
{
	Health.SubtractHealth(Damage);
}
```

### Became

``` csharp
public FloatReference Damage;

void DealDamage()
{
	Health.SubtractHealth(Damage);
}
```

## Static Scriptable Objects

**Static Scriptable Object** is a feature that allows referencing serialized values in code in singleton-like way

### Usage
To create a **Static Scriptable Object** you simply define a class with the following signature (or change the existing scriptable object) 

```csharp
[CreateAssetMenu(...)]
class YourClass : StaticScriptableObject<YourClass>
```

Then you can create an asset instance of `YourClass` and set the reference to the corresponding field in `desExt -> Static Scriptable Objects Manager` menu

Later in code you can access the `YourClass` instance simply calling `Yourclass.Instance`

## Presets

**Preset** is a set of all the configurations provided in **desExt** and can be switched in `desExt -> Preset Manager`

# Supported types

- AnimationClip
- bool
- Color
- Float
- GameObject
- int
- LayerMask
- Matrix4x4
- Rect
- string
- Texture2D
- Vector2
- Vector3
- Vector4
