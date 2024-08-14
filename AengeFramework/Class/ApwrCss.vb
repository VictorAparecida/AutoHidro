Public Class ApwrCss

    Public Function RetCSS(ByVal typeCss As String) As String

        Select Case LCase(typeCss)
            Case "AHID"
                RetCSS = "JCPAKFTCGADH"

            Case "aenge"
                RetCSS = "AP2FLV32X"

            Case "ahid"
                RetCSS = "OPJCPAEH2"

            Case Else
                RetCSS = "JCPAKFTCGADH"

        End Select

    End Function

End Class
