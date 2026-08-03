using System.Numerics;
using Raylib_cs;

const int screenWidth = 800;
const int screenHeight = 600;
const int paddleWidth = 16;
const int paddleHeight = 100;
const int ballSize = 16;
const float paddleSpeed = 400f;

Raylib.InitWindow(screenWidth, screenHeight, "Pong");
Raylib.SetTargetFPS(60);

var leftPaddle = new Rectangle(40, screenHeight / 2 - paddleHeight / 2, paddleWidth, paddleHeight);
var rightPaddle = new Rectangle(screenWidth - 40 - paddleWidth, screenHeight / 2 - paddleHeight / 2, paddleWidth, paddleHeight);
var paddleColor = new Color(2, 48, 71);
var ballPosition = new Vector2(screenWidth / 2, screenHeight / 2);
var ballVel = new Vector2(350, 250);

int leftScore = 0;
int rightScore = 0;
var scoreColor = new Color(251, 133, 0);
// kommentar

while (!Raylib.WindowShouldClose())
{
   float dt = Raylib.GetFrameTime();
   if (Raylib.IsKeyDown(KeyboardKey.W)) leftPaddle.Y -= paddleSpeed * dt;
   if (Raylib.IsKeyDown(KeyboardKey.S)) leftPaddle.Y += paddleSpeed * dt;
   if (Raylib.IsKeyDown(KeyboardKey.Up)) rightPaddle.Y -= paddleSpeed * dt;
   if (Raylib.IsKeyDown(KeyboardKey.Down)) rightPaddle.Y += paddleSpeed * dt;

leftPaddle.Y = Math.Clamp(leftPaddle.Y, 0, screenHeight - paddleHeight);
rightPaddle.Y = Math.Clamp(rightPaddle.Y, 0, screenHeight - paddleHeight);

ballPosition += ballVel * dt;

if (ballPosition.Y - ballSize / 2 <= 0 || ballPosition.Y + ballSize / 2 >=screenHeight )
ballVel.Y = -ballVel.Y;

if (Raylib.CheckCollisionCircleRec(ballPosition, ballSize / 2, leftPaddle) && ballVel.X < 0)
ballVel.X = -ballVel.X;
if (Raylib.CheckCollisionCircleRec(ballPosition, ballSize / 2, rightPaddle) && ballVel.X > 0)
ballVel.X = -ballVel.X;

if (ballPosition.X < 0) {rightScore ++; ResetBall();}
if (ballPosition.X > screenWidth) {leftScore ++; ResetBall();}

Raylib.BeginDrawing();
Raylib.ClearBackground(Color.Black);

Raylib.DrawRectangleRec(leftPaddle, paddleColor);
Raylib.DrawRectangleRec(rightPaddle, paddleColor);
Raylib.DrawCircleV(ballPosition, ballSize / 2, Color.Blue);

Raylib.DrawText($"{leftScore}", screenWidth / 2 -60, 20, 40, scoreColor);
Raylib.DrawText($"{rightScore}", screenWidth / 2 + 40, 20, 40, scoreColor);

Raylib.EndDrawing();
}
Raylib.CloseWindow();


void ResetBall()
{
   ballPosition = new Vector2(screenWidth / 2, screenHeight / 2);
   ballVel.X = -ballVel.X;
}