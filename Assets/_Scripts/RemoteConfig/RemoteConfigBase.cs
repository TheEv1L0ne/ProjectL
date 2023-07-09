using Unity.Services.RemoteConfig;

public abstract class RemoteConfigBase<T>
{
    public RemoteConfigBase()
    {
        var typeT = typeof(T);
        var namePrefix = typeT.FullName;

        
        var properties = typeT.GetProperties();

        foreach (var property in properties)
        {
            if(!RemoteConfigService.Instance.appConfig.HasKey($"{namePrefix}.{property.Name}")) continue;
            var propertysType = property.PropertyType;
            if (propertysType == typeof(int))
            {
                property.SetValue(this, RemoteConfigService.Instance.appConfig.GetInt($"{namePrefix}.{property.Name}"));
                continue;
            }

            if (propertysType == typeof(string))
            {
                property.SetValue(this, RemoteConfigService.Instance.appConfig.GetString($"{namePrefix}.{property.Name}"));
                continue;
            }

            if (propertysType == typeof(bool))
            {
                property.SetValue(this, RemoteConfigService.Instance.appConfig.GetBool($"{namePrefix}.{property.Name}"));
                continue;
            }

            if (propertysType == typeof(float))
            {
                property.SetValue(this, RemoteConfigService.Instance.appConfig.GetFloat($"{namePrefix}.{property.Name}"));
                continue;
            }
        }


        var fields = typeT.GetFields();
        foreach (var field in fields)
        {
            if(!RemoteConfigService.Instance.appConfig.HasKey($"{namePrefix}.{field.Name}")) continue;
            var fieldsType = field.FieldType;
            if (fieldsType == typeof(int))
            {
                field.SetValue(this, RemoteConfigService.Instance.appConfig.GetInt($"{namePrefix}.{field.Name}"));
                continue;
            }

            if (fieldsType == typeof(string))
            {
                field.SetValue(this, RemoteConfigService.Instance.appConfig.GetString($"{namePrefix}.{field.Name}"));
                continue;
            }

            if (fieldsType == typeof(bool))
            {
                field.SetValue(this, RemoteConfigService.Instance.appConfig.GetBool($"{namePrefix}.{field.Name}"));
                continue;
            }

            if (fieldsType == typeof(float))
            {
                field.SetValue(this, RemoteConfigService.Instance.appConfig.GetFloat($"{namePrefix}.{field.Name}"));
                continue;
            }
        }
    }
}