// Toggles AMD Adrenalin Instant Replay by flipping HKCU\Software\AMD\DVR\InstantReplayEnabled.
// AMDRSServ.exe watches that key and starts/stops the replay buffer live.
// Build: run build.cmd
using Microsoft.Win32;

static class ToggleReplay
{
    static void Main()
    {
        using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\AMD\DVR"))
        {
            object current = key.GetValue("InstantReplayEnabled", 0);
            int next = (current is int && (int)current != 0) ? 0 : 1;
            key.SetValue("InstantReplayEnabled", next, RegistryValueKind.DWord);
        }
    }
}
