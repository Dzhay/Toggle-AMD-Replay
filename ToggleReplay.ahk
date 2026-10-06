; Toggles AMD Adrenalin Instant Replay with Ctrl+Alt+Right.
; Requires AutoHotkey v2: https://www.autohotkey.com/
#Requires AutoHotkey v2.0
#SingleInstance Force

^!Right:: {
    key := "HKCU\Software\AMD\DVR"
    enabled := RegRead(key, "InstantReplayEnabled", 0)
    RegWrite(enabled ? 0 : 1, "REG_DWORD", key, "InstantReplayEnabled")
}
