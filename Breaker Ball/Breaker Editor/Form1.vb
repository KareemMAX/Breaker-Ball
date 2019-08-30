Imports System.ComponentModel
Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Public Class Form1
    Dim Blocks(14, 13) As Byte

    Dim Loaded As Boolean

#Disable Warning BC40000

    Private Sub GlControl_Paint(sender As Object, e As PaintEventArgs) Handles GlControl.Paint
        GL.Clear(ClearBufferMask.ColorBufferBit)

        GL.ClearColor(Color.Black)

        If Not Loaded Then
            GL.Viewport(0, 0, GlControl.Width, GlControl.Height)
            GL.MatrixMode(MatrixMode.Projection)
            Dim Aspect As Single = GlControl.Width / GlControl.Height
            GL.Ortho(-Aspect, Aspect, -1, 1, -1, 1)
            GL.MatrixMode(MatrixMode.Modelview)
            GL.LoadIdentity()
            Loaded = True
        End If

        GL.LineWidth(2.5)

        GL.Begin(BeginMode.LineLoop)

        GL.Color3(Color.White)

        GL.Vertex2(-1.51, -1)
        GL.Vertex2(-1.51, 0.91)
        GL.Vertex2(1.51, 0.91)
        GL.Vertex2(1.51, -1)

        GL.End()

        DrawBlocks()

        Const X As SByte = 1
        Const Y As SByte = -4
        If TrackBar1.Value + 1 = 1 OrElse TrackBar1.Value + 1 = 6 OrElse TrackBar1.Value + 1 = 11 Then
            GL.Begin(BeginMode.Quads)

            If TrackBar1.Value + 1 = 1 Then
                GL.Color3(Color.FromArgb(0, 255, 0))
            ElseIf TrackBar1.Value + 1 = 6 Then
                GL.Color3(Color.FromArgb(255, 0, 0))
            ElseIf TrackBar1.Value + 1 = 11 Then
                GL.Color3(Color.FromArgb(0, 0, 255))
            End If

            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)
            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

            GL.End()

            GL.Begin(BeginMode.LineLoop)

            If TrackBar1.Value + 1 = 1 Then
                GL.Color3(Color.GreenYellow)
            ElseIf TrackBar1.Value + 1 = 6 Then
                GL.Color3(Color.HotPink)
            ElseIf TrackBar1.Value + 1 = 11 Then
                GL.Color3(Color.CadetBlue)
            End If

            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)
            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

            GL.End()
        ElseIf TrackBar1.Value + 1 = 2 OrElse TrackBar1.Value + 1 = 7 OrElse TrackBar1.Value + 1 = 12 Then
            GL.Begin(BeginMode.Triangles)

            If TrackBar1.Value + 1 = 2 Then
                GL.Color3(Color.FromArgb(0, 255, 0))
            ElseIf TrackBar1.Value + 1 = 7 Then
                GL.Color3(Color.FromArgb(255, 0, 0))
            ElseIf TrackBar1.Value + 1 = 12 Then
                GL.Color3(Color.FromArgb(0, 0, 255))
            End If

            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)

            GL.End()

            GL.Begin(BeginMode.LineLoop)

            If TrackBar1.Value + 1 = 2 Then
                GL.Color3(Color.GreenYellow)
            ElseIf TrackBar1.Value + 1 = 7 Then
                GL.Color3(Color.HotPink)
            ElseIf TrackBar1.Value + 1 = 12 Then
                GL.Color3(Color.CadetBlue)
            End If

            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)

            GL.End()
        ElseIf TrackBar1.Value + 1 = 3 OrElse TrackBar1.Value + 1 = 8 OrElse TrackBar1.Value + 1 = 13 Then
            GL.Begin(BeginMode.Triangles)

            If TrackBar1.Value + 1 = 3 Then
                GL.Color3(Color.FromArgb(0, 255, 0))
            ElseIf TrackBar1.Value + 1 = 8 Then
                GL.Color3(Color.FromArgb(255, 0, 0))
            ElseIf TrackBar1.Value + 1 = 13 Then
                GL.Color3(Color.FromArgb(0, 0, 255))
            End If

            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

            GL.End()

            GL.Begin(BeginMode.LineLoop)

            If TrackBar1.Value + 1 = 3 Then
                GL.Color3(Color.GreenYellow)
            ElseIf TrackBar1.Value + 1 = 8 Then
                GL.Color3(Color.HotPink)
            ElseIf TrackBar1.Value + 1 = 13 Then
                GL.Color3(Color.CadetBlue)
            End If

            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

            GL.End()
        ElseIf TrackBar1.Value + 1 = 4 OrElse TrackBar1.Value + 1 = 9 OrElse TrackBar1.Value + 1 = 14 Then
            GL.Begin(BeginMode.Triangles)

            If TrackBar1.Value + 1 = 4 Then
                GL.Color3(Color.FromArgb(0, 255, 0))
            ElseIf TrackBar1.Value + 1 = 9 Then
                GL.Color3(Color.FromArgb(255, 0, 0))
            ElseIf TrackBar1.Value + 1 = 14 Then
                GL.Color3(Color.FromArgb(0, 0, 255))
            End If

            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)
            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

            GL.End()

            GL.Begin(BeginMode.LineLoop)

            If TrackBar1.Value + 1 = 4 Then
                GL.Color3(Color.GreenYellow)
            ElseIf TrackBar1.Value + 1 = 9 Then
                GL.Color3(Color.HotPink)
            ElseIf TrackBar1.Value + 1 = 14 Then
                GL.Color3(Color.CadetBlue)
            End If

            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)
            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

            GL.End()
        ElseIf TrackBar1.Value + 1 = 5 OrElse TrackBar1.Value + 1 = 10 OrElse TrackBar1.Value + 1 = 15 Then
            GL.Begin(BeginMode.Triangles)

            If TrackBar1.Value + 1 = 5 Then
                GL.Color3(Color.FromArgb(0, 255, 0))
            ElseIf TrackBar1.Value + 1 = 10 Then
                GL.Color3(Color.FromArgb(255, 0, 0))
            ElseIf TrackBar1.Value + 1 = 15 Then
                GL.Color3(Color.FromArgb(0, 0, 255))
            End If

            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)
            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

            GL.End()

            GL.Begin(BeginMode.LineLoop)

            If TrackBar1.Value + 1 = 5 Then
                GL.Color3(Color.GreenYellow)
            ElseIf TrackBar1.Value + 1 = 10 Then
                GL.Color3(Color.HotPink)
            ElseIf TrackBar1.Value + 1 = 15 Then
                GL.Color3(Color.CadetBlue)
            End If

            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
            GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)
            GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

            GL.End()
        End If

        GlControl.SwapBuffers()
    End Sub

    Sub DrawBlocks()

        For Y As Byte = 0 To UBound(Blocks, 2)
            For X As Byte = 0 To UBound(Blocks)
                If Blocks(X, Y) = 1 OrElse Blocks(X, Y) = 6 OrElse Blocks(X, Y) = 11 Then
                    GL.Begin(BeginMode.Quads)

                    If Blocks(X, Y) = 1 Then
                        GL.Color3(Color.FromArgb(0, 255, 0))
                    ElseIf Blocks(X, Y) = 6 Then
                        GL.Color3(Color.FromArgb(255, 0, 0))
                    ElseIf Blocks(X, Y) = 11 Then
                        GL.Color3(Color.FromArgb(0, 0, 255))
                    End If

                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)
                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

                    GL.End()

                    GL.Begin(BeginMode.LineLoop)

                    If Blocks(X, Y) = 1 Then
                        GL.Color3(Color.GreenYellow)
                    ElseIf Blocks(X, Y) = 6 Then
                        GL.Color3(Color.HotPink)
                    ElseIf Blocks(X, Y) = 11 Then
                        GL.Color3(Color.CadetBlue)
                    End If

                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)
                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

                    GL.End()
                ElseIf Blocks(X, Y) = 2 OrElse Blocks(X, Y) = 7 OrElse Blocks(X, Y) = 12 Then
                    GL.Begin(BeginMode.Triangles)

                    If Blocks(X, Y) = 2 Then
                        GL.Color3(Color.FromArgb(0, 255, 0))
                    ElseIf Blocks(X, Y) = 7 Then
                        GL.Color3(Color.FromArgb(255, 0, 0))
                    ElseIf Blocks(X, Y) = 12 Then
                        GL.Color3(Color.FromArgb(0, 0, 255))
                    End If

                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)

                    GL.End()

                    GL.Begin(BeginMode.LineLoop)

                    If Blocks(X, Y) = 2 Then
                        GL.Color3(Color.GreenYellow)
                    ElseIf Blocks(X, Y) = 7 Then
                        GL.Color3(Color.HotPink)
                    ElseIf Blocks(X, Y) = 12 Then
                        GL.Color3(Color.CadetBlue)
                    End If

                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)

                    GL.End()
                ElseIf Blocks(X, Y) = 3 OrElse Blocks(X, Y) = 8 OrElse Blocks(X, Y) = 13 Then
                    GL.Begin(BeginMode.Triangles)

                    If Blocks(X, Y) = 3 Then
                        GL.Color3(Color.FromArgb(0, 255, 0))
                    ElseIf Blocks(X, Y) = 8 Then
                        GL.Color3(Color.FromArgb(255, 0, 0))
                    ElseIf Blocks(X, Y) = 13 Then
                        GL.Color3(Color.FromArgb(0, 0, 255))
                    End If

                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

                    GL.End()

                    GL.Begin(BeginMode.LineLoop)

                    If Blocks(X, Y) = 3 Then
                        GL.Color3(Color.GreenYellow)
                    ElseIf Blocks(X, Y) = 8 Then
                        GL.Color3(Color.HotPink)
                    ElseIf Blocks(X, Y) = 13 Then
                        GL.Color3(Color.CadetBlue)
                    End If

                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

                    GL.End()
                ElseIf Blocks(X, Y) = 4 OrElse Blocks(X, Y) = 9 OrElse Blocks(X, Y) = 14 Then
                    GL.Begin(BeginMode.Triangles)

                    If Blocks(X, Y) = 4 Then
                        GL.Color3(Color.FromArgb(0, 255, 0))
                    ElseIf Blocks(X, Y) = 9 Then
                        GL.Color3(Color.FromArgb(255, 0, 0))
                    ElseIf Blocks(X, Y) = 14 Then
                        GL.Color3(Color.FromArgb(0, 0, 255))
                    End If

                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)
                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

                    GL.End()

                    GL.Begin(BeginMode.LineLoop)

                    If Blocks(X, Y) = 4 Then
                        GL.Color3(Color.GreenYellow)
                    ElseIf Blocks(X, Y) = 9 Then
                        GL.Color3(Color.HotPink)
                    ElseIf Blocks(X, Y) = 14 Then
                        GL.Color3(Color.CadetBlue)
                    End If

                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)
                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

                    GL.End()
                ElseIf Blocks(X, Y) = 5 OrElse Blocks(X, Y) = 10 OrElse Blocks(X, Y) = 15 Then
                    GL.Begin(BeginMode.Triangles)

                    If Blocks(X, Y) = 5 Then
                        GL.Color3(Color.FromArgb(0, 255, 0))
                    ElseIf Blocks(X, Y) = 10 Then
                        GL.Color3(Color.FromArgb(255, 0, 0))
                    ElseIf Blocks(X, Y) = 15 Then
                        GL.Color3(Color.FromArgb(0, 0, 255))
                    End If

                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)
                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

                    GL.End()

                    GL.Begin(BeginMode.LineLoop)

                    If Blocks(X, Y) = 5 Then
                        GL.Color3(Color.GreenYellow)
                    ElseIf Blocks(X, Y) = 10 Then
                        GL.Color3(Color.HotPink)
                    ElseIf Blocks(X, Y) = 15 Then
                        GL.Color3(Color.CadetBlue)
                    End If

                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y))
                    GL.Vertex2(-1.5 + (0.2 * X) + 0.2, -0.5 + (0.1 * Y) + 0.1)
                    GL.Vertex2(-1.5 + (0.2 * X), -0.5 + (0.1 * Y) + 0.1)

                    GL.End()
                End If
            Next
        Next
    End Sub

    Private Function getNormalisedDeviceCoordinates(x As Integer, y As Integer) As Vector2
        Dim x2 As Single = (3.2F * x) / GlControl.Width - 1.6F
        Dim y2 As Single = -((2.0F * y) / GlControl.Height - 1.0F)
        Return New Vector2(x2, y2)
    End Function

    Private Sub GlControl_MouseDown(sender As Object, e As MouseEventArgs) Handles GlControl.MouseDown
        Dim Normalised As Vector2 = getNormalisedDeviceCoordinates(e.X, e.Y)
        If Normalised.X < -1.5 OrElse Normalised.X > 1.5 OrElse Normalised.Y < -0.5 OrElse Normalised.Y > 0.9 Then Exit Sub
        Normalised.X = Int((Normalised.X + 1.5) / 0.2)
        Normalised.Y = Int((Normalised.Y + 0.5) / 0.1)
        If Blocks(Normalised.X, Normalised.Y) = 0 Then
            Blocks(Normalised.X, Normalised.Y) = TrackBar1.Value + 1
        Else
            Blocks(Normalised.X, Normalised.Y) = 0
        End If
        GlControl.Refresh()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveFileDialog.ShowDialog()
    End Sub

    Private Sub SaveFileDialog_FileOk(sender As Object, e As CancelEventArgs) Handles SaveFileDialog.FileOk
        Dim No As Integer = FreeFile()
        Try
            FileOpen(No, SaveFileDialog.FileName, OpenMode.Binary, OpenAccess.Default)

            FilePut(No, Blocks)
        Catch
            MsgBox("Can't save the level")
        Finally
            FileClose(No)
            SaveFileDialog.FileName = ""
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenFileDialog.ShowDialog()
        GlControl.Refresh()
    End Sub

    Private Sub OpenFileDialog_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog.FileOk
        Dim No As Integer = FreeFile()
        Try
            FileOpen(No, OpenFileDialog.FileName, OpenMode.Binary, OpenAccess.Default)

            FileGet(No, Blocks)
        Catch
            MsgBox("Can't load the level")
        Finally
            FileClose(No)
            OpenFileDialog.FileName = ""
        End Try
    End Sub

    ''That made me wonder a little bit :"D
    ''This is to generate level intialzing code automaticly from the level editor. Once it finish it copies the code to my clipboard to paste it anywhere else
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Code As String = "ReDim Blocks(14, 13)" & vbCrLf
        For Y As Byte = 0 To UBound(Blocks, 2)
            For X As Byte = 0 To UBound(Blocks)
                If Blocks(X, Y) <> 0 Then
                    Code &= "Blocks(" & X & ", " & Y & ") = " & Blocks(X, Y) & vbCrLf
                End If
            Next
        Next
        Clipboard.SetText(Code)
        MsgBox("Copied to clipboard")
    End Sub

    Private Sub TrackBar1_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar1.ValueChanged
        GlControl.Refresh()
    End Sub
End Class
