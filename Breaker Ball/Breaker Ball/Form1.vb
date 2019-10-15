Imports System.Drawing.Imaging
Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Public Class Form1
    Dim IsInStartup As Boolean = True
    Dim IsInLevels As Boolean
    Dim AboutShow As Boolean
    Dim IsStarted As Boolean
    Dim IsPaused As Boolean

    Dim IsMouseHidden As Boolean
    Dim OldLoc As Point

    Dim Plate As Single = 0 'MAX 12
    Dim PlateSize As Byte = 3
    Dim Blocks(14, 13) As Byte
    Dim Ball As New Vector2(0, -0.65)

    Dim BallRotation As Single = AngleConverter(90)

    Dim Lives As Byte = 3
    Dim Score As Integer
    Dim Combo As Integer

    Dim Loaded As Boolean

    Dim IsInLevel As Boolean
    Dim CurrentLevel As Byte

    Dim IsWinner As Boolean
    Dim IsLoser As Boolean

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

        If Not IsInStartup AndAlso Not IsInLevels Then
            GL.Enable(EnableCap.Texture2D)

            LoadTex(My.Resources.Score)

            GL.Begin(BeginMode.Quads)

            GL.Color3(Color.White)

            GL.TexCoord2(0, 1)
            GL.Vertex2(-1.5, 0.93)
            GL.TexCoord2(1, 1)
            GL.Vertex2(-1.23, 0.93)
            GL.TexCoord2(1, 0)
            GL.Vertex2(-1.23, 0.99)
            GL.TexCoord2(0, 0)
            GL.Vertex2(-1.5, 0.99)

            GL.End()

            LoadTex(GetGraphicsNumber(Score))

            GL.Begin(BeginMode.Quads)

            GL.Color3(Color.White)

            GL.TexCoord2(0, 1)
            GL.Vertex2(-1.2, 0.93)
            GL.TexCoord2(1, 1)
            GL.Vertex2(-0.84, 0.93)
            GL.TexCoord2(1, 0)
            GL.Vertex2(-0.84, 0.99)
            GL.TexCoord2(0, 0)
            GL.Vertex2(-1.2, 0.99)

            GL.End()

            LoadTex(My.Resources.Lives)

            GL.Begin(BeginMode.Quads)

            GL.Color3(Color.White)

            GL.TexCoord2(0, 1)
            GL.Vertex2(0, 0.93)
            GL.TexCoord2(1, 1)
            GL.Vertex2(0.26, 0.93)
            GL.TexCoord2(1, 0)
            GL.Vertex2(0.26, 0.99)
            GL.TexCoord2(0, 0)
            GL.Vertex2(0, 0.99)

            GL.End()

            GL.Disable(EnableCap.Texture2D)

            If Lives >= 1 Then
                GL.Begin(BeginMode.Quads)

                GL.Color3(Color.White)

                GL.TexCoord2(0, 1)
                GL.Vertex2(0.28, 0.93)
                GL.TexCoord2(1, 1)
                GL.Vertex2(0.6, 0.93)
                GL.TexCoord2(1, 0)
                GL.Vertex2(0.6, 0.99)
                GL.TexCoord2(0, 0)
                GL.Vertex2(0.28, 0.99)

                GL.End()
            End If

            If Lives >= 2 Then
                GL.Begin(BeginMode.Quads)

                GL.Color3(Color.White)

                GL.TexCoord2(0, 1)
                GL.Vertex2(0.65, 0.93)
                GL.TexCoord2(1, 1)
                GL.Vertex2(0.97, 0.93)
                GL.TexCoord2(1, 0)
                GL.Vertex2(0.97, 0.99)
                GL.TexCoord2(0, 0)
                GL.Vertex2(0.65, 0.99)

                GL.End()
            End If

            If Lives >= 3 Then
                GL.Begin(BeginMode.Quads)

                GL.Color3(Color.White)

                GL.TexCoord2(0, 1)
                GL.Vertex2(1.02, 0.93)
                GL.TexCoord2(1, 1)
                GL.Vertex2(1.34, 0.93)
                GL.TexCoord2(1, 0)
                GL.Vertex2(1.34, 0.99)
                GL.TexCoord2(0, 0)
                GL.Vertex2(1.02, 0.99)

                GL.End()
            End If

        End If

        GL.LineWidth(2.5)

        GL.Begin(BeginMode.LineLoop)

        GL.Color3(Color.White)

        GL.Vertex2(-1.51, -1)
        GL.Vertex2(-1.51, 0.91)
        GL.Vertex2(1.51, 0.91)
        GL.Vertex2(1.51, -1)

        GL.End()

        GL.Enable(EnableCap.Texture2D)

        LoadTex(My.Resources.Ball)

        GL.Begin(BeginMode.Quads)

        GL.TexCoord2(0, 1)
        GL.Vertex2(Ball.X - 0.05, Ball.Y - 0.05)
        GL.TexCoord2(1, 1)
        GL.Vertex2(Ball.X + 0.05, Ball.Y - 0.05)
        GL.TexCoord2(1, 0)
        GL.Vertex2(Ball.X + 0.05, Ball.Y + 0.05)
        GL.TexCoord2(0, 0)
        GL.Vertex2(Ball.X - 0.05, Ball.Y + 0.05)

        GL.End()
        GL.Disable(EnableCap.Texture2D)

        GL.Begin(BeginMode.Quads)

        GL.Color3(Color.White)

        GL.Vertex2(-(PlateSize * 0.1) + (Plate * 0.1), -0.8)
        GL.Vertex2(PlateSize * 0.1 + (Plate * 0.1), -0.8)
        GL.Vertex2(PlateSize * 0.1 + (Plate * 0.1), -0.7)
        GL.Vertex2(-(PlateSize * 0.1) + (Plate * 0.1), -0.7)

        GL.End()

        DrawBlocks()

        If IsInStartup OrElse IsPaused OrElse IsInLevels Then

            GL.Enable(EnableCap.Blend)
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)
            GL.BlendEquation(BlendEquationMode.FuncAdd)

            GL.Begin(BeginMode.Quads)

            GL.Color4(Color.FromArgb(150, 0, 0, 0))
            GL.Vertex2(-2, -1)
            GL.Vertex2(2, -1)
            GL.Vertex2(2, 1)
            GL.Vertex2(-2, 1)

            GL.End()

            GL.Disable(EnableCap.Blend)
            If IsPaused Then
                GL.Enable(EnableCap.Texture2D)
                GL.Enable(EnableCap.Blend)
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)
                GL.BlendEquation(BlendEquationMode.Max)

                LoadTex(My.Resources.Pause)

                GL.Begin(BeginMode.Quads)

                GL.Color3(Color.White)

                GL.TexCoord2(0, 1)
                GL.Vertex2(-0.6, -0.1)
                GL.TexCoord2(1, 1)
                GL.Vertex2(0.6, -0.1)
                GL.TexCoord2(1, 0)
                GL.Vertex2(0.6, 0.5)
                GL.TexCoord2(0, 0)
                GL.Vertex2(-0.6, 0.5)

                GL.End()

                GL.Disable(EnableCap.Texture2D)
                GL.Disable(EnableCap.Blend)
            ElseIf IsInStartup Then
                GL.Enable(EnableCap.Texture2D)
                GL.Enable(EnableCap.Blend)
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)
                GL.BlendEquation(BlendEquationMode.Max)

                LoadTex(My.Resources.Title)

                GL.Begin(BeginMode.Quads)

                GL.Color3(Color.White)

                GL.TexCoord2(0, 1)
                GL.Vertex2(-0.6, 0.2)
                GL.TexCoord2(1, 1)
                GL.Vertex2(0.6, 0.2)
                GL.TexCoord2(1, 0)
                GL.Vertex2(0.6, 0.8)
                GL.TexCoord2(0, 0)
                GL.Vertex2(-0.6, 0.8)

                GL.End()

                LoadTex(My.Resources.About)

                GL.Begin(BeginMode.Quads)

                GL.Color3(Color.White)

                GL.TexCoord2(0, 1)
                GL.Vertex2(1, -0.9)
                GL.TexCoord2(1, 1)
                GL.Vertex2(1.4, -0.9)
                GL.TexCoord2(1, 0)
                GL.Vertex2(1.4, -0.7)
                GL.TexCoord2(0, 0)
                GL.Vertex2(1, -0.7)

                GL.End()

                GL.Disable(EnableCap.Blend)

                Dim Pos As Vector2 = getNormalisedDeviceCoordinates(GlControl.PointToClient(Cursor.Position).X, GlControl.PointToClient(Cursor.Position).Y)

                If Pos.X < 0.4 AndAlso Pos.X > -0.4 AndAlso Pos.Y < 0.1 AndAlso Pos.Y > -0.3 Then
                    LoadTex(My.Resources.StartInverted)
                Else
                    LoadTex(My.Resources.Start)
                End If

                GL.Begin(BeginMode.Quads)

                GL.Color3(Color.White)

                GL.TexCoord2(0, 1)
                GL.Vertex2(-0.4, -0.3)
                GL.TexCoord2(1, 1)
                GL.Vertex2(0.4, -0.3)
                GL.TexCoord2(1, 0)
                GL.Vertex2(0.4, 0.1)
                GL.TexCoord2(0, 0)
                GL.Vertex2(-0.4, 0.1)

                GL.End()

                GL.Disable(EnableCap.Texture2D)
            ElseIf IsInLevels Then
                GL.Enable(EnableCap.Blend)
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)
                GL.BlendEquation(BlendEquationMode.Max)
                GL.Enable(EnableCap.Texture2D)

                LoadTex(My.Resources.Level1)

                GL.Begin(BeginMode.Quads)

                GL.Color3(Color.White)

                GL.TexCoord2(0, 1)
                GL.Vertex2(-0.4, 0.5)
                GL.TexCoord2(1, 1)
                GL.Vertex2(0.4, 0.5)
                GL.TexCoord2(1, 0)
                GL.Vertex2(0.4, 0.9)
                GL.TexCoord2(0, 0)
                GL.Vertex2(-0.4, 0.9)

                GL.End()

                LoadTex(My.Resources.Level2)

                GL.Begin(BeginMode.Quads)

                GL.Color3(Color.White)

                GL.TexCoord2(0, 1)
                GL.Vertex2(-0.4, 0.2)
                GL.TexCoord2(1, 1)
                GL.Vertex2(0.4, 0.2)
                GL.TexCoord2(1, 0)
                GL.Vertex2(0.4, 0.6)
                GL.TexCoord2(0, 0)
                GL.Vertex2(-0.4, 0.6)

                GL.End()

                LoadTex(My.Resources.Level3)

                GL.Begin(BeginMode.Quads)

                GL.Color3(Color.White)

                GL.TexCoord2(0, 1)
                GL.Vertex2(-0.4, -0.1)
                GL.TexCoord2(1, 1)
                GL.Vertex2(0.4, -0.1)
                GL.TexCoord2(1, 0)
                GL.Vertex2(0.4, 0.3)
                GL.TexCoord2(0, 0)
                GL.Vertex2(-0.4, 0.3)

                GL.End()

                LoadTex(My.Resources.Level4)

                GL.Begin(BeginMode.Quads)

                GL.Color3(Color.White)

                GL.TexCoord2(0, 1)
                GL.Vertex2(-0.4, -0.4)
                GL.TexCoord2(1, 1)
                GL.Vertex2(0.4, -0.4)
                GL.TexCoord2(1, 0)
                GL.Vertex2(0.4, 0)
                GL.TexCoord2(0, 0)
                GL.Vertex2(-0.4, 0)

                GL.End()

                LoadTex(My.Resources.Browse)

                GL.Begin(BeginMode.Quads)

                GL.Color3(Color.White)

                GL.TexCoord2(0, 1)
                GL.Vertex2(-0.4, -0.7)
                GL.TexCoord2(1, 1)
                GL.Vertex2(0.4, -0.7)
                GL.TexCoord2(1, 0)
                GL.Vertex2(0.4, -0.3)
                GL.TexCoord2(0, 0)
                GL.Vertex2(-0.4, -0.3)

                GL.End()

                GL.Disable(EnableCap.Blend)
                GL.Disable(EnableCap.Texture2D)
            End If
        End If

        If IsWinner Then
            GL.Enable(EnableCap.Texture2D)
            GL.Enable(EnableCap.AlphaTest)

            LoadTex(My.Resources.Won)

            GL.Begin(BeginMode.Quads)

            GL.Color3(Color.White)

            GL.TexCoord2(0, 1)
            GL.Vertex2(-0.7, -0.7)
            GL.TexCoord2(1, 1)
            GL.Vertex2(0.7, -0.7)
            GL.TexCoord2(1, 0)
            GL.Vertex2(0.7, 0.7)
            GL.TexCoord2(0, 0)
            GL.Vertex2(-0.7, 0.7)

            GL.End()

            GL.Disable(EnableCap.AlphaTest)

            LoadTex(GetGraphicsNumber(Score))

            GL.Enable(EnableCap.Blend)
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)
            GL.BlendEquation(BlendEquationMode.Max)

            GL.Begin(BeginMode.Quads)

            GL.Color3(Color.White)

            GL.TexCoord2(0, 1)
            GL.Vertex2(-0.5, -0.5)
            GL.TexCoord2(1, 1)
            GL.Vertex2(0.5, -0.5)
            GL.TexCoord2(1, 0)
            GL.Vertex2(0.5, -0.3)
            GL.TexCoord2(0, 0)
            GL.Vertex2(-0.5, -0.3)

            GL.End()

            GL.Disable(EnableCap.Blend)
            GL.Disable(EnableCap.Texture2D)
        ElseIf IsLoser Then
            GL.Enable(EnableCap.Texture2D)
            GL.Enable(EnableCap.AlphaTest)

            LoadTex(My.Resources.Lose)

            GL.Begin(BeginMode.Quads)

            GL.Color3(Color.White)

            GL.TexCoord2(0, 1)
            GL.Vertex2(-0.7, -0.7)
            GL.TexCoord2(1, 1)
            GL.Vertex2(0.7, -0.7)
            GL.TexCoord2(1, 0)
            GL.Vertex2(0.7, 0.7)
            GL.TexCoord2(0, 0)
            GL.Vertex2(-0.7, 0.7)

            GL.End()

            GL.Disable(EnableCap.AlphaTest)

            LoadTex(GetGraphicsNumber(Score))

            GL.Enable(EnableCap.Blend)
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha)
            GL.BlendEquation(BlendEquationMode.Max)

            GL.Begin(BeginMode.Quads)

            GL.Color3(Color.White)

            GL.TexCoord2(0, 1)
            GL.Vertex2(-0.5, -0.5)
            GL.TexCoord2(1, 1)
            GL.Vertex2(0.5, -0.5)
            GL.TexCoord2(1, 0)
            GL.Vertex2(0.5, -0.3)
            GL.TexCoord2(0, 0)
            GL.Vertex2(-0.5, -0.3)

            GL.End()

            GL.Disable(EnableCap.Blend)
            GL.Disable(EnableCap.Texture2D)
        End If

        If AboutShow Then
            GL.Enable(EnableCap.Texture2D)

            LoadTex(My.Resources.AboutDialog)

            GL.Begin(BeginMode.Quads)

            GL.Color3(Color.White)

            GL.TexCoord2(0, 1)
            GL.Vertex2(-0.7, -0.7)
            GL.TexCoord2(1, 1)
            GL.Vertex2(0.7, -0.7)
            GL.TexCoord2(1, 0)
            GL.Vertex2(0.7, 0.7)
            GL.TexCoord2(0, 0)
            GL.Vertex2(-0.7, 0.7)

            GL.End()

            GL.Disable(EnableCap.Texture2D)
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

        IsInStartup = False
        IsInLevels = False

        HideMouse()
        Plate = 0
        Ball = New Vector2(0, -0.65)
        BallRotation = AngleConverter(90)
        StartUp.Stop()
        Timer.Start()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        If IsWinner OrElse IsLoser Then Exit Sub

        Dim n As Vector2 = getNormalisedDeviceCoordinates(GlControl.PointToClient(Cursor.Position).X, GlControl.PointToClient(Cursor.Position).Y)
        If IsStarted AndAlso Not IsPaused Then Plate = n.X / 0.1
        If Plate < -12 Then
            Plate = -12
        ElseIf Plate > 12 Then
            Plate = 12
        End If

        If IsMouseHidden Then
            Dim pos As Point = GlControl.PointToClient(Cursor.Position)
            If pos.X > GlControl.Width Then
                Cursor.Position = GlControl.PointToScreen(New Point(GlControl.Width, pos.Y))
            ElseIf pos.X < 0 Then
                Cursor.Position = GlControl.PointToScreen(New Point(0, pos.Y))
            ElseIf pos.Y > GlControl.Height Then
                Cursor.Position = GlControl.PointToScreen(New Point(pos.X, GlControl.Height))
            ElseIf pos.Y < 0 Then
                Cursor.Position = GlControl.PointToScreen(New Point(pos.X, 0))
            End If
        End If

        If IsPaused Then Exit Sub

        Dim v As New Vector2(Math.Cos(BallRotation), Math.Sin(BallRotation))

        If IsStarted Then Ball += 0.03F * v

        If Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) = 0
            End If
            BallRotation += 2 * (AngleConverter(180) - BallRotation)
            Combo += 1
            Score += 50 * Combo
            My.Computer.Audio.Play(My.Resources.Hit1, AudioPlayMode.Background)
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) = 0
            End If
            BallRotation += 2 * (AngleConverter(180) - BallRotation)
            Combo += 1
            Score += 50 * Combo
            My.Computer.Audio.Play(My.Resources.Hit1, AudioPlayMode.Background)
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) = 0
            End If
            BallRotation = AngleConverter(360 - (90 + (2 * (RAngleConverter(BallRotation)) + (180 - (RAngleConverter(BallRotation) + 90)))))
            Combo += 1
            Score += 50 * Combo
            My.Computer.Audio.Play(My.Resources.Hit1, AudioPlayMode.Background)
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) = 0
            End If
            BallRotation = AngleConverter(360 - (90 + (2 * (RAngleConverter(BallRotation)) + (180 - (RAngleConverter(BallRotation) + 90)))))
            Combo += 1
            Score += 50 * Combo
            My.Computer.Audio.Play(My.Resources.Hit1, AudioPlayMode.Background)
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) = 0
            End If
            BallRotation += AngleConverter(180)
            Combo += 1
            Score += 50 * Combo
            My.Computer.Audio.Play(My.Resources.Hit1, AudioPlayMode.Background)
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) = 0
            End If
            BallRotation += AngleConverter(180)
            Combo += 1
            Score += 50 * Combo
            My.Computer.Audio.Play(My.Resources.Hit1, AudioPlayMode.Background)
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) = 0
            End If
            BallRotation += AngleConverter(180)
            Combo += 1
            Score += 50 * Combo
            My.Computer.Audio.Play(My.Resources.Hit1, AudioPlayMode.Background)
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) = 0
            End If
            BallRotation += AngleConverter(180)
            Combo += 1
            Score += 50 * Combo
            My.Computer.Audio.Play(My.Resources.Hit1, AudioPlayMode.Background)
        End If

        If Ball.Y > 0.85 Then
            BallRotation += 2 * (AngleConverter(180) - BallRotation)
            My.Computer.Audio.Play(My.Resources.Hit2, AudioPlayMode.Background)
        End If

        If Ball.X > 1.46 Then
            Ball = New Vector2(1.44, Ball.Y)
            BallRotation = AngleConverter(360 - (90 + (2 * RAngleConverter(BallRotation)) + (180 - (RAngleConverter(BallRotation) + 90))))
            My.Computer.Audio.Play(My.Resources.Hit2, AudioPlayMode.Background)
        End If

        If Ball.X < -1.46 Then
            Ball = New Vector2(-1.44, Ball.Y)
            BallRotation = AngleConverter(360 - (90 + (2 * RAngleConverter(BallRotation)) + (180 - (RAngleConverter(BallRotation) + 90))))
            My.Computer.Audio.Play(My.Resources.Hit2, AudioPlayMode.Background)
        End If

        If Ball.Y > 1 OrElse Ball.X > 2 OrElse Ball.X < -2 Then
            Timer.Stop()
            MsgBox("The ball leave the zone", MsgBoxStyle.Critical)
            ShowMouse()
            IsStarted = False
            Plate = 0
            Ball = New Vector2(0, -0.65)
            BallRotation = AngleConverter(90)
            Timer.Start()
        End If

        If Ball.Y < -0.6 AndAlso Ball.Y > -0.8 Then
            If Not Ball.X < -(PlateSize * 0.1) + (Plate * 0.1) AndAlso Not Ball.X > (PlateSize * 0.1) + (Plate * 0.1) Then
                BallRotation = AngleConverter(-(Ball.X - (Plate * 0.1)) * 200 + 90)
                Combo = 0
                If IsStarted Then My.Computer.Audio.Play(My.Resources.Hit2, AudioPlayMode.Background)
            End If
        End If

        If Ball.Y < -1.2 Then
            Timer.Stop()
            Lives -= 1
            Score -= 200
            Combo = 0
            If Score < 0 Then Score = 0
            GlControl.Refresh()
            If Lives = 0 Then
                IsLoser = True
                IsInLevel = False
                My.Computer.Audio.Play(My.Resources.Loser, AudioPlayMode.Background)
                GlControl.Refresh()
                ShowMouse()
            Else
                IsStarted = False
                Plate = 0
                Ball = New Vector2(0, -0.65)
                BallRotation = AngleConverter(90)
                Timer.Start()
            End If
        End If

        Dim Empty = True

        For Y As Byte = 0 To UBound(Blocks, 2)
            For X As Byte = 0 To UBound(Blocks)
                If Blocks(X, Y) <> 0 Then
                    Empty = False
                End If
            Next
        Next

        If Empty Then
            If IsInLevel Then
                CurrentLevel += 1
                Combo = 0
                Plate = 0
                Ball = New Vector2(0, -0.65)
                BallRotation = AngleConverter(90)
                Select Case CurrentLevel
                    Case 2
                        IsStarted = False
                        SetupLevel2()
                    Case 3
                        IsStarted = False
                        SetupLevel3()
                    Case 4
                        IsStarted = False
                        SetupLevel4()
                    Case 5
                        IsInLevel = False
                        IsWinner = True
                        Combo = 0
                        My.Computer.Audio.Play(My.Resources.Winner, AudioPlayMode.Background)
                        GlControl.Refresh()
                        ShowMouse()
                End Select
            Else
                IsWinner = True
                Combo = 0
                My.Computer.Audio.Play(My.Resources.Winner, AudioPlayMode.Background)
                GlControl.Refresh()
                ShowMouse()
            End If
        End If

        GlControl.Refresh()
    End Sub

    Function AngleConverter(Angle As Single) As Single
        Const X As Single = Math.PI / 180
        Return X * Angle
    End Function

    Function RAngleConverter(Angle As Single) As Single
        Const X As Single = 180 / Math.PI
        Return X * Angle
    End Function

    Private Sub StartUp_Tick(sender As Object, e As EventArgs) Handles StartUp.Tick
        Plate = CInt(Ball.X / 0.1)
        If Plate > 12 Then
            Plate = 12
        ElseIf Plate < -12 Then
            Plate = -12
        End If

        Dim v As New Vector2(Math.Cos(BallRotation), Math.Sin(BallRotation))

        Ball += 0.03F * v

        If Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) = 0
            End If
            BallRotation += 2 * (AngleConverter(180) - BallRotation)
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) = 0
            End If
            BallRotation += 2 * (AngleConverter(180) - BallRotation)
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) = 0
            End If
            BallRotation = AngleConverter(360 - (90 + (2 * (RAngleConverter(BallRotation)) + (180 - (RAngleConverter(BallRotation) + 90)))))
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.5) / 0.1)) = 0
            End If
            BallRotation = AngleConverter(360 - (90 + (2 * (RAngleConverter(BallRotation)) + (180 - (RAngleConverter(BallRotation) + 90)))))
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) = 0
            End If
            BallRotation += AngleConverter(180)
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X + 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) = 0
            End If
            BallRotation += AngleConverter(180)
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y + 0.05 + 0.5) / 0.1)) = 0
            End If
            BallRotation += AngleConverter(180)
        ElseIf Not (Ball.X < -1.45 OrElse Ball.X > 1.45 OrElse Ball.Y < -0.45 OrElse Ball.Y > 0.85) AndAlso (Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) <> 0) Then
            If Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) > 5 Then
                Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) -= 5
            Else
                Blocks(Int((Ball.X - 0.05 + 1.5) / 0.2), Int((Ball.Y - 0.05 + 0.5) / 0.1)) = 0
            End If
            BallRotation += AngleConverter(180)
        End If

        If Ball.Y > 0.85 Then
            BallRotation += 2 * (AngleConverter(180) - BallRotation)
        End If

        If Ball.X > 1.46 Then
            Ball = New Vector2(1.44, Ball.Y)
            BallRotation = AngleConverter(360 - (90 + (2 * RAngleConverter(BallRotation) + (180 - (RAngleConverter(BallRotation) + 90)))))
        End If

        If Ball.X < -1.46 Then
            Ball = New Vector2(-1.44, Ball.Y)
            BallRotation = AngleConverter(360 - (90 + (2 * RAngleConverter(BallRotation) + (180 - (RAngleConverter(BallRotation) + 90)))))
        End If

        If Ball.Y > 1 OrElse Ball.X > 2 OrElse Ball.X < -2 Then
            Plate = 0
            Ball = New Vector2(0, -0.65)
            BallRotation = AngleConverter(90)
        End If

        If Ball.Y < -0.6 AndAlso Ball.Y > -0.8 Then
            If Not Ball.X < -0.3 + (Plate * 0.1) AndAlso Not Ball.X > 0.3 + (Plate * 0.1) Then
                BallRotation = AngleConverter(-(Ball.X - (Plate * 0.1)) * 200 + 90)
            End If
        End If

        If Ball.Y < -1.2 Then
            Plate = 0
            Ball = New Vector2(0, -0.65)
            BallRotation = AngleConverter(90)
            SetupLevel1()
        End If

        Dim Empty = True

        For Y As Byte = 0 To UBound(Blocks, 2)
            For X As Byte = 0 To UBound(Blocks)
                If Blocks(X, Y) <> 0 Then
                    Empty = False
                End If
            Next
        Next

        If Empty Then
            Plate = 0
            Ball = New Vector2(0, -0.65)
            BallRotation = AngleConverter(90)
            SetupLevel1()
        End If

        GlControl.Refresh()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetupLevel1()
    End Sub

    Sub LoadTex(Texture As Bitmap)
        Dim Tex As Integer = 1
        Dim pic As Bitmap = Texture
        GL.BindTexture(TextureTarget.Texture2D, Tex)
        Dim data As BitmapData = pic.LockBits(New Rectangle(0, 0, pic.Width, pic.Height),
   ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppPArgb)
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, pic.Width, pic.Height, 0,
    Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0)
        pic.UnlockBits(data)
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D)
        GL.TexParameter(TextureTarget.ProxyTexture2D, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear)
        GL.TexParameter(TextureTarget.ProxyTexture2D, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear)
        pic.Dispose()

        GL.BindTexture(TextureTarget.ProxyTexture2D, Tex)
    End Sub

    Private Function getNormalisedDeviceCoordinates(x As Integer, y As Integer) As Vector2
        Dim x2 As Single = (3.2F * x) / GlControl.Width - 1.6F
        Dim y2 As Single = -((2.0F * y) / GlControl.Height - 1.0F)
        Return New Vector2(x2, y2)
    End Function

    Sub SetupLevel1()
        ReDim Blocks(14, 13)
        Blocks(7, 5) = 1
        Blocks(6, 6) = 1
        Blocks(7, 6) = 1
        Blocks(8, 6) = 1
        Blocks(5, 7) = 1
        Blocks(6, 7) = 1
        Blocks(7, 7) = 1
        Blocks(8, 7) = 1
        Blocks(9, 7) = 1
        Blocks(4, 8) = 1
        Blocks(5, 8) = 1
        Blocks(6, 8) = 1
        Blocks(8, 8) = 1
        Blocks(9, 8) = 1
        Blocks(10, 8) = 1
        Blocks(2, 9) = 1
        Blocks(3, 9) = 1
        Blocks(4, 9) = 1
        Blocks(5, 9) = 1
        Blocks(6, 9) = 1
        Blocks(8, 9) = 1
        Blocks(9, 9) = 1
        Blocks(10, 9) = 1
        Blocks(11, 9) = 1
        Blocks(12, 9) = 1
        Blocks(1, 10) = 1
        Blocks(2, 10) = 1
        Blocks(3, 10) = 1
        Blocks(5, 10) = 1
        Blocks(6, 10) = 1
        Blocks(7, 10) = 1
        Blocks(8, 10) = 1
        Blocks(9, 10) = 1
        Blocks(11, 10) = 1
        Blocks(12, 10) = 1
        Blocks(13, 10) = 1
        Blocks(2, 11) = 1
        Blocks(3, 11) = 1
        Blocks(4, 11) = 1
        Blocks(5, 11) = 1
        Blocks(6, 11) = 1
        Blocks(7, 11) = 1
        Blocks(8, 11) = 1
        Blocks(9, 11) = 1
        Blocks(10, 11) = 1
        Blocks(11, 11) = 1
        Blocks(12, 11) = 1
        Blocks(2, 12) = 2
        Blocks(4, 12) = 1
        Blocks(7, 12) = 1
        Blocks(10, 12) = 1
        Blocks(12, 12) = 3
        Blocks(4, 13) = 2
        Blocks(6, 13) = 4
        Blocks(8, 13) = 5
        Blocks(10, 13) = 3
    End Sub

    Sub SetupLevel2()
        ReDim Blocks(14, 13)
        Blocks(3, 5) = 6
        Blocks(4, 5) = 6
        Blocks(5, 5) = 6
        Blocks(6, 5) = 6
        Blocks(7, 5) = 6
        Blocks(8, 5) = 6
        Blocks(9, 5) = 6
        Blocks(10, 5) = 6
        Blocks(11, 5) = 6
        Blocks(2, 6) = 1
        Blocks(3, 6) = 1
        Blocks(5, 6) = 1
        Blocks(6, 6) = 1
        Blocks(7, 6) = 6
        Blocks(8, 6) = 1
        Blocks(9, 6) = 1
        Blocks(11, 6) = 1
        Blocks(12, 6) = 1
        Blocks(1, 7) = 6
        Blocks(2, 7) = 6
        Blocks(3, 7) = 6
        Blocks(4, 7) = 6
        Blocks(5, 7) = 6
        Blocks(6, 7) = 6
        Blocks(7, 7) = 1
        Blocks(8, 7) = 6
        Blocks(9, 7) = 6
        Blocks(10, 7) = 6
        Blocks(11, 7) = 6
        Blocks(12, 7) = 6
        Blocks(13, 7) = 6
        Blocks(2, 8) = 1
        Blocks(5, 8) = 6
        Blocks(6, 8) = 1
        Blocks(8, 8) = 1
        Blocks(9, 8) = 6
        Blocks(12, 8) = 1
        Blocks(2, 9) = 1
        Blocks(4, 9) = 6
        Blocks(5, 9) = 1
        Blocks(9, 9) = 1
        Blocks(10, 9) = 6
        Blocks(12, 9) = 1
        Blocks(2, 10) = 1
        Blocks(3, 10) = 6
        Blocks(4, 10) = 1
        Blocks(10, 10) = 1
        Blocks(11, 10) = 6
        Blocks(12, 10) = 1
        Blocks(2, 11) = 6
        Blocks(3, 11) = 1
        Blocks(11, 11) = 1
        Blocks(12, 11) = 6
        Blocks(1, 12) = 6
        Blocks(2, 12) = 1
        Blocks(12, 12) = 1
        Blocks(13, 12) = 6
        Blocks(5, 13) = 1
        Blocks(6, 13) = 1
        Blocks(7, 13) = 1
        Blocks(8, 13) = 1
        Blocks(9, 13) = 1
    End Sub

    Sub SetupLevel3()
        ReDim Blocks(14, 13)
        Blocks(6, 1) = 1
        Blocks(7, 1) = 1
        Blocks(8, 1) = 1
        Blocks(0, 2) = 1
        Blocks(14, 2) = 1
        Blocks(0, 3) = 1
        Blocks(1, 3) = 1
        Blocks(7, 3) = 1
        Blocks(13, 3) = 1
        Blocks(14, 3) = 1
        Blocks(0, 4) = 6
        Blocks(1, 4) = 1
        Blocks(7, 4) = 1
        Blocks(13, 4) = 1
        Blocks(14, 4) = 6
        Blocks(1, 5) = 1
        Blocks(4, 5) = 1
        Blocks(7, 5) = 1
        Blocks(10, 5) = 1
        Blocks(13, 5) = 1
        Blocks(0, 6) = 6
        Blocks(1, 6) = 1
        Blocks(3, 6) = 1
        Blocks(4, 6) = 11
        Blocks(5, 6) = 1
        Blocks(7, 6) = 1
        Blocks(9, 6) = 1
        Blocks(10, 6) = 11
        Blocks(11, 6) = 1
        Blocks(13, 6) = 1
        Blocks(14, 6) = 6
        Blocks(1, 7) = 1
        Blocks(3, 7) = 1
        Blocks(4, 7) = 11
        Blocks(5, 7) = 1
        Blocks(7, 7) = 1
        Blocks(9, 7) = 1
        Blocks(10, 7) = 11
        Blocks(11, 7) = 1
        Blocks(13, 7) = 1
        Blocks(0, 8) = 6
        Blocks(1, 8) = 1
        Blocks(4, 8) = 1
        Blocks(7, 8) = 1
        Blocks(10, 8) = 1
        Blocks(13, 8) = 1
        Blocks(14, 8) = 6
        Blocks(1, 9) = 1
        Blocks(7, 9) = 1
        Blocks(13, 9) = 1
        Blocks(0, 10) = 6
        Blocks(1, 10) = 1
        Blocks(2, 10) = 4
        Blocks(6, 10) = 5
        Blocks(7, 10) = 1
        Blocks(8, 10) = 4
        Blocks(12, 10) = 5
        Blocks(13, 10) = 1
        Blocks(14, 10) = 6
        Blocks(1, 11) = 1
        Blocks(2, 11) = 1
        Blocks(3, 11) = 1
        Blocks(4, 11) = 1
        Blocks(5, 11) = 1
        Blocks(6, 11) = 1
        Blocks(7, 11) = 1
        Blocks(8, 11) = 1
        Blocks(9, 11) = 1
        Blocks(10, 11) = 1
        Blocks(11, 11) = 1
        Blocks(12, 11) = 1
        Blocks(13, 11) = 1
        Blocks(0, 12) = 6
        Blocks(2, 12) = 11
        Blocks(4, 12) = 11
        Blocks(6, 12) = 11
        Blocks(8, 12) = 11
        Blocks(10, 12) = 11
        Blocks(12, 12) = 11
        Blocks(14, 12) = 6
        Blocks(1, 13) = 6
        Blocks(2, 13) = 6
        Blocks(3, 13) = 6
        Blocks(4, 13) = 6
        Blocks(5, 13) = 6
        Blocks(6, 13) = 6
        Blocks(7, 13) = 6
        Blocks(8, 13) = 6
        Blocks(9, 13) = 6
        Blocks(10, 13) = 6
        Blocks(11, 13) = 6
        Blocks(12, 13) = 6
        Blocks(13, 13) = 6
    End Sub

    Sub SetupLevel4()
        ReDim Blocks(14, 13)
        Blocks(2, 3) = 11
        Blocks(3, 3) = 6
        Blocks(4, 3) = 11
        Blocks(5, 3) = 6
        Blocks(6, 3) = 11
        Blocks(7, 3) = 6
        Blocks(8, 3) = 11
        Blocks(9, 3) = 6
        Blocks(10, 3) = 11
        Blocks(11, 3) = 6
        Blocks(12, 3) = 11
        Blocks(2, 4) = 6
        Blocks(3, 4) = 1
        Blocks(4, 4) = 1
        Blocks(5, 4) = 11
        Blocks(6, 4) = 1
        Blocks(7, 4) = 1
        Blocks(8, 4) = 1
        Blocks(9, 4) = 11
        Blocks(10, 4) = 1
        Blocks(11, 4) = 1
        Blocks(12, 4) = 6
        Blocks(2, 5) = 11
        Blocks(3, 5) = 1
        Blocks(4, 5) = 1
        Blocks(5, 5) = 1
        Blocks(6, 5) = 11
        Blocks(7, 5) = 6
        Blocks(8, 5) = 11
        Blocks(9, 5) = 1
        Blocks(10, 5) = 1
        Blocks(11, 5) = 1
        Blocks(12, 5) = 11
        Blocks(2, 6) = 6
        Blocks(3, 6) = 1
        Blocks(4, 6) = 1
        Blocks(5, 6) = 1
        Blocks(6, 6) = 1
        Blocks(7, 6) = 1
        Blocks(8, 6) = 1
        Blocks(9, 6) = 1
        Blocks(10, 6) = 1
        Blocks(11, 6) = 1
        Blocks(12, 6) = 6
        Blocks(2, 7) = 11
        Blocks(3, 7) = 1
        Blocks(4, 7) = 1
        Blocks(5, 7) = 1
        Blocks(6, 7) = 1
        Blocks(7, 7) = 11
        Blocks(8, 7) = 1
        Blocks(9, 7) = 1
        Blocks(10, 7) = 1
        Blocks(11, 7) = 1
        Blocks(12, 7) = 11
        Blocks(2, 8) = 6
        Blocks(3, 8) = 1
        Blocks(4, 8) = 1
        Blocks(5, 8) = 1
        Blocks(6, 8) = 1
        Blocks(7, 8) = 6
        Blocks(8, 8) = 1
        Blocks(9, 8) = 1
        Blocks(10, 8) = 1
        Blocks(11, 8) = 1
        Blocks(12, 8) = 6
        Blocks(2, 9) = 11
        Blocks(3, 9) = 1
        Blocks(4, 9) = 1
        Blocks(5, 9) = 1
        Blocks(6, 9) = 1
        Blocks(7, 9) = 11
        Blocks(8, 9) = 1
        Blocks(9, 9) = 1
        Blocks(10, 9) = 1
        Blocks(11, 9) = 1
        Blocks(12, 9) = 11
        Blocks(2, 10) = 6
        Blocks(3, 10) = 1
        Blocks(4, 10) = 1
        Blocks(5, 10) = 11
        Blocks(6, 10) = 1
        Blocks(7, 10) = 1
        Blocks(8, 10) = 1
        Blocks(9, 10) = 11
        Blocks(10, 10) = 1
        Blocks(11, 10) = 1
        Blocks(12, 10) = 6
        Blocks(2, 11) = 11
        Blocks(3, 11) = 1
        Blocks(4, 11) = 11
        Blocks(5, 11) = 6
        Blocks(6, 11) = 11
        Blocks(7, 11) = 1
        Blocks(8, 11) = 11
        Blocks(9, 11) = 6
        Blocks(10, 11) = 11
        Blocks(11, 11) = 1
        Blocks(12, 11) = 11
        Blocks(2, 12) = 6
        Blocks(3, 12) = 1
        Blocks(4, 12) = 1
        Blocks(5, 12) = 11
        Blocks(6, 12) = 1
        Blocks(7, 12) = 1
        Blocks(8, 12) = 1
        Blocks(9, 12) = 11
        Blocks(10, 12) = 1
        Blocks(11, 12) = 1
        Blocks(12, 12) = 6
        Blocks(2, 13) = 11
        Blocks(3, 13) = 1
        Blocks(4, 13) = 1
        Blocks(5, 13) = 1
        Blocks(6, 13) = 1
        Blocks(7, 13) = 1
        Blocks(8, 13) = 1
        Blocks(9, 13) = 1
        Blocks(10, 13) = 1
        Blocks(11, 13) = 1
        Blocks(12, 13) = 11
    End Sub

    Private Sub GlControl_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GlControl.MouseDown
        If e.Button = MouseButtons.Left Then
            If AboutShow Then
                AboutShow = False
                Exit Sub
            End If

            If Timer.Enabled AndAlso Not IsStarted Then
                IsStarted = True
                Cursor.Position = GlControl.PointToScreen(New Point(GlControl.Width / 2, 50))
            End If

            Dim Pos As Vector2 = getNormalisedDeviceCoordinates(e.X, e.Y)

            If IsInLevels Then
                If Pos.X < 0.4 AndAlso Pos.X > -0.4 AndAlso Pos.Y < 0.9 AndAlso Pos.Y > 0.5 Then
                    IsInStartup = False
                    IsInLevels = False

                    HideMouse()
                    SetupLevel1()
                    IsInLevel = True
                    CurrentLevel = 1
                    Plate = 0
                    Ball = New Vector2(0, -0.65)
                    BallRotation = AngleConverter(90)
                    StartUp.Stop()
                    Timer.Start()
                ElseIf Pos.X < 0.4 AndAlso Pos.X > -0.4 AndAlso Pos.Y < 0.6 AndAlso Pos.Y > 0.2 Then
                    IsInStartup = False
                    IsInLevels = False


                    HideMouse()
                    SetupLevel2()
                    IsInLevel = True
                    CurrentLevel = 2
                    Plate = 0
                    Ball = New Vector2(0, -0.65)
                    BallRotation = AngleConverter(90)
                    StartUp.Stop()
                    Timer.Start()
                ElseIf Pos.X < 0.4 AndAlso Pos.X > -0.4 AndAlso Pos.Y < 0.3 AndAlso Pos.Y > -0.1 Then
                    IsInStartup = False
                    IsInLevels = False

                    HideMouse()
                    SetupLevel3()
                    IsInLevel = True
                    CurrentLevel = 3
                    Plate = 0
                    Ball = New Vector2(0, -0.65)
                    BallRotation = AngleConverter(90)
                    StartUp.Stop()
                    Timer.Start()
                ElseIf Pos.X < 0.4 AndAlso Pos.X > -0.4 AndAlso Pos.Y < 0 AndAlso Pos.Y > -0.4 Then
                    IsInStartup = False
                    IsInLevels = False

                    HideMouse()
                    SetupLevel4()
                    IsInLevel = True
                    CurrentLevel = 4
                    Plate = 0
                    Ball = New Vector2(0, -0.65)
                    BallRotation = AngleConverter(90)
                    StartUp.Stop()
                    Timer.Start()
                ElseIf Pos.X < 0.4 AndAlso Pos.X > -0.4 AndAlso Pos.Y < -0.3 AndAlso Pos.Y > -0.7 Then
                    OpenFileDialog.ShowDialog()
                End If
            End If

            If IsInStartup Then
                If Pos.X < 0.4 AndAlso Pos.X > -0.4 AndAlso Pos.Y < 0.1 AndAlso Pos.Y > -0.3 Then
                    IsInStartup = False
                    IsInLevels = True
                ElseIf Pos.X < 1.4 AndAlso Pos.X > 1 AndAlso Pos.Y < -0.7 AndAlso Pos.Y > -0.9 Then
                    AboutShow = True
                End If
            End If

            If IsWinner Then
                IsWinner = False
                Timer.Stop()
                Lives = 3
                Score = 0
                IsStarted = False
                IsInStartup = True
                Plate = 0
                Ball = New Vector2(0, -0.65)
                BallRotation = AngleConverter(90)
                SetupLevel1()
                StartUp.Start()
            ElseIf IsLoser Then
                IsLoser = False
                Lives = 3
                Score = 0
                IsStarted = False
                IsInStartup = True
                Plate = 0
                Ball = New Vector2(0, -0.65)
                BallRotation = AngleConverter(90)
                SetupLevel1()
                StartUp.Start()
            End If
        End If
    End Sub

    Function GetGraphicsNumber(Num As Integer)
        Dim BM As New Bitmap(150, 25)
        Dim G As Drawing.Graphics = Drawing.Graphics.FromImage(BM)
        G.Clear(Color.Transparent)
        G.DrawString(Num, New Font("Arial Narrow", 19), New SolidBrush(Color.White), New Point(0, 0))
        Return BM
    End Function

    Private Sub GlControl_KeyUp(sender As Object, e As KeyEventArgs) Handles GlControl.KeyUp
        If e.KeyData = Keys.Escape AndAlso Timer.Enabled Then
            IsPaused = Not IsPaused
            If IsPaused Then
                ShowMouse()
                OldLoc = Cursor.Position
            Else
                HideMouse()
                Cursor.Position = OldLoc
            End If
            GlControl.Refresh()
        End If
    End Sub

    Sub HideMouse()
        If Not IsMouseHidden Then
            Cursor.Hide()
            IsMouseHidden = True
        End If
    End Sub

    Sub ShowMouse()
        If IsMouseHidden Then
            Cursor.Show()
            IsMouseHidden = False
        End If
    End Sub
End Class
