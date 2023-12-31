﻿using Newtonsoft.Json;
public class SaveLoad
{
    string pathToGameFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents") + "/WarThunder";


    public SaveLoad()
    {
        CreateDirectory();
    }

    private void CreateDirectory()
    {
        Directory.CreateDirectory(pathToGameFiles);
        Directory.CreateDirectory($"{pathToGameFiles}/Saves");
    }
    
    public void SaveProfiles(Profiles profiles)
    {
        if (profiles.currentProfile != null)
        {
            //profiles.profiles[FindProfileIndex(profiles)] = profiles.currentProfile;
        }

        int profileIndex = FindProfileIndex(profiles, profiles.currentProfile);

        profiles.profiles[profileIndex].SetValuesFromAnotherProfile(profiles.currentProfile);

        StreamWriter SW = new StreamWriter($"{pathToGameFiles}/Saves/Profiles.json");

        string serializedData = JsonConvert.SerializeObject(profiles, Formatting.Indented);
        SW.WriteLine(serializedData);

        SW.Close();
    }
    public Profiles LoadAllProfiles()
    {
        try
        {
            return JsonConvert.DeserializeObject<Profiles>(File.ReadAllText($"{pathToGameFiles}/Saves/Profiles.json"));
        }
        catch
        {
            if (File.ReadAllText($"{pathToGameFiles}/Saves/Profiles.json").Length == 0)
            {
                SaveProfiles(new Profiles(new Profile[0]));
            }
            else
            {
                return new Profiles(new Profile[0]);
            }
        }
        return JsonConvert.DeserializeObject<Profiles>(File.ReadAllText($"{pathToGameFiles}/Saves/Profiles.json"));
    }
    public int FindProfileIndex(Profiles profiles, Profile profile)
    {
        for (int i = 0; i < profiles.profiles.Count; i++)
        {
            if (profiles.profiles[i].Login == profile.Login && profiles.profiles[i].Password == profile.Password)
            {
                return i;
            }
        }
        return 0;
    }




}
