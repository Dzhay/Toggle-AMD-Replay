# Toggle Replay

Toggle AMD Adrenalin **Instant Replay** on and off with a hotkey, without opening
AMD Software.

## About

Adrenalin has a hotkey to *save* an instant replay, but none to turn the
feature on or off. If you don't want the replay buffer running all the time -
you have to open the GUI every time.

Instant Replay is controlled by one registry value:

```
HKCU\Software\AMD\DVR\InstantReplayEnabled
```

The AMD service (`AMDRSServ.exe`) watches this key. When the value changes,
it starts or stops the replay buffer and shows the usual
"Instant Replay On/Off" notification. AMD Software does not need to be open.

This was found by recording the GUI toggle with
[Process Monitor](https://learn.microsoft.com/sysinternals/downloads/procmon).

## Option 1: ToggleReplay.exe + shortcut hotkey

A tiny Windows program that flips the
value each time it runs.

1. Download `ToggleReplay.exe` from the
   [Releases](../../releases) page, or build it yourself (see below).
2. Put it in a folder where it can stay.
3. Right-click `ToggleReplay.exe` and choose **Show more options** >
   **Send to** > **Desktop (create shortcut)**.
4. Right-click the new shortcut and choose **Properties**.
5. Click the **Shortcut key** field and press your hotkey, for example
   `Ctrl+Alt+Right`. Click **OK**.

The shortcut must stay on the Desktop or in the Start Menu for the hotkey to
work. If you move or rename the `.exe`, create the shortcut again.

## Option 2: AutoHotkey

An alternative to Option 1 that reacts to the hotkey faster. It needs
[AutoHotkey v2](https://www.autohotkey.com/) installed (v1 will not work).

**Run the included script**

1. Download [`ToggleReplay.ahk`](ToggleReplay.ahk).
2. Double-click it. Nothing opens, but a green **H** icon appears in the
   system tray. The script now runs in the background.
3. Press `Ctrl+Alt+Right` to toggle Instant Replay.

The hotkey works only while the script is running. To stop it, right-click the
tray icon and choose **Exit**.

To run it automatically when Windows starts, press `Win+R`, type
`shell:startup`, and press Enter. Put a shortcut to `ToggleReplay.ahk` in the
folder that opens.

**Change the hotkey**

Open `ToggleReplay.ahk` in a text editor and change `^!Right` in the line
`^!Right:: {`. `^` is Ctrl, `!` is Alt, `+` is
Shift, and `#` is Win. For example, `^+F9` is `Ctrl+Shift+F9`. Save the file
and double-click it again to reload. See the
[AutoHotkey hotkey docs](https://www.autohotkey.com/docs/v2/Hotkeys.htm) for
all key names.

**Already have your own AutoHotkey script?**

Paste this into it instead of running `ToggleReplay.ahk`:

```autohotkey
^!Right:: {
    key := "HKCU\Software\AMD\DVR"
    enabled := RegRead(key, "InstantReplayEnabled", 0)
    RegWrite(enabled ? 0 : 1, "REG_DWORD", key, "InstantReplayEnabled")
}
```

## Option 3: Command line

Turn on:

```bat
reg add "HKCU\Software\AMD\DVR" /v InstantReplayEnabled /t REG_DWORD /d 1 /f
```

Turn off:

```bat
reg add "HKCU\Software\AMD\DVR" /v InstantReplayEnabled /t REG_DWORD /d 0 /f
```

## Building

```bat
build.cmd
```

This compiles `ToggleReplay.cs` to `ToggleReplay.exe` with the C# compiler
that ships with Windows (.NET Framework 4.x). Nothing else needs to be
installed.

## Notes

- `AMDRSServ.exe` must be running. It starts with Windows when AMD Software is
  installed.
- Tested with AMD Software: Adrenalin Edition on Windows 11. A future driver
  update could rename the value.
- Not affiliated with or endorsed by AMD.

## License

[MIT](LICENSE)
