using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AnimationCtrl
{
    public static void AnimateImage(RectTransform imageRect, Sprite[] spritesAnimations, float time) {
        LeanTween.play(imageRect, spritesAnimations).setTime(time).setLoopClamp();
    }

    public static void AnimateImage(CanvasGroup canvasGroup, float alpha, float time, Action callback)
    {
        LeanTween.alphaCanvas(canvasGroup, alpha, time).setLoopClamp().setOnComplete(callback).setOnCompleteOnRepeat(true);
    }

    public static void ModifyAlphaWithLoop(CanvasGroup canvasGroup, float alpha, float time)
    {
        LeanTween.alphaCanvas(canvasGroup, alpha, time).setLoopClamp();
    }

    public static void DotsAnimation(CanvasGroup canvasGroup, float alpha, float time, RectTransform rectTransform, Vector2 position, float delayTime)
    {
        LeanTween.alphaCanvas(canvasGroup, alpha, time).setLoopPingPong();
        LeanTween.scale(rectTransform, position, time).setLoopPingPong();
    }

    #region Move
    public static void MoveDiagonal(GameObject gameObject, float xPosition, float yPosition, float time)
    {
        LeanTween.moveLocal(gameObject, new Vector3(xPosition, yPosition, 0f), time);
    }

    public static void MoveDiagonal(GameObject gameObject, float xPosition, float yPosition, float time, Action callback)
    {
        LeanTween.moveLocal(gameObject, new Vector3(xPosition, yPosition, 0f), time).setOnComplete(callback);
    }

    public static void MoveHorizontal(GameObject gameObject, float xPosition, float time)
    {
        LeanTween.moveLocalX(gameObject, xPosition, time);
    }

    public static void MoveUI(RectTransform rectTransform, Vector2 position, float time)
    {
        LeanTween.move(rectTransform, position, time);
    }

    public static void MoveUI(RectTransform rectTransform, Vector2 position, float time, Action callBAck)
    {
        LeanTween.move(rectTransform, position, time).setOnComplete(callBAck);
    }
    #endregion

    #region Scale
    public static void ScaleObject(GameObject gameObject, float scaleHorizontal, float scaleVertical, float time)
    {
        LeanTween.scale(gameObject, new Vector3(scaleHorizontal, scaleVertical, 0f), time);
    }

    public static void ScaleObject(GameObject gameObject, float scaleHorizontal, float scaleVertical, float time, Action callBack)
    {
        LeanTween.scale(gameObject, new Vector3(scaleHorizontal, scaleVertical, 0f), time).setOnComplete(callBack);
    }

    public static void ScaleXObject(GameObject gameObject, float horizontalScale, float time)
    {
        LeanTween.scaleX(gameObject, horizontalScale, time);
    }

    public static void ScaleXObject(GameObject gameObject, float horizontalScale, float time, Action callBack)
    {
        LeanTween.scaleX(gameObject, horizontalScale, time).setOnComplete(callBack);
    }

    public static void ScaleUI(RectTransform rectTransform, Vector2 position, float time)
    {
        LeanTween.scale(rectTransform, position, time);
    }

    public static void ScaleUI(RectTransform rectTransform, Vector2 position, float time, Action callBack)
    {
        LeanTween.scale(rectTransform, position, time).setOnComplete(callBack);
    }
    #endregion

    #region Canvas Alpha
    

    public static void ModifyAlpha(CanvasGroup canvasGroup, float alpha, float time)
    {
        LeanTween.alphaCanvas(canvasGroup, alpha, time);
    }

    public static void ModifyAlpha(CanvasGroup canvasGroup, float alpha, float time, Action callback)
    {
        LeanTween.alphaCanvas(canvasGroup, alpha, time).setOnComplete(callback);
    }

    public static void ModifyAlpha(CanvasGroup canvasGroup, float alpha, float time, float delayTime)
    {
        LeanTween.alphaCanvas(canvasGroup, alpha, time).setDelay(delayTime);
    }

    public static void ModifyAlpha(CanvasGroup canvasGroup, float alpha, float time, float delayTime, Action callback)
    {
        LeanTween.alphaCanvas(canvasGroup, alpha, time).setDelay(delayTime).setOnComplete(callback);
    }
    #endregion

    public static void MoveCanvas(GameObject gameObject, CanvasGroup canvasGroup, float horizontalPosition, float alpha, float time)
    {
        MoveHorizontal(gameObject, horizontalPosition, time);
        ModifyAlpha(canvasGroup, alpha, time);
    }


    public static void MoveCanvas(GameObject gameObject, CanvasGroup canvasGroup, float horizontalPosition, float alpha, float time, Action callBack)
    {
        MoveHorizontal(gameObject, horizontalPosition, time);
        ModifyAlpha(canvasGroup, alpha, time, callBack);
    }

    public static void MoveCanvas(RectTransform rectTransform, CanvasGroup canvasGroup, Vector2 position, float alpha, float time)
    {
        MoveUI(rectTransform, position, time);
        ModifyAlpha(canvasGroup, alpha, time);
    }


    public static void MoveCanvas(RectTransform rectTransform, CanvasGroup canvasGroup, Vector2 position, float alpha, float time, Action callBack)
    {
        MoveUI(rectTransform, position, time);
        ModifyAlpha(canvasGroup, alpha, time, callBack);
    }
}
