﻿$("#mypanorama").ipanorama({
    "theme": "ipnrm-theme-default",
    "autoLoad": true,
    "autoRotate": false,
    "autoRotateSpeed": 0.001,
    "autoRotateInactivityDelay": 3000,
    "mouseWheelPreventDefault": true,
    "mouseWheelRotate": false,
    "mouseWheelRotateCoef": 0.2,
    "mouseWheelZoom": true,
    "mouseWheelZoomCoef": 0.05,
    "hoverGrab": false,
    "hoverGrabYawCoef": 20,
    "hoverGrabPitchCoef": 20,
    "grab": true,
    "grabCoef": 0.1,
    "pinchZoom": true,
    "pinchZoomCoef": 0.1,
    "showControlsOnHover": false,
    "showSceneThumbsCtrl": false,
    "showSceneMenuCtrl": false,
    "showSceneNextPrevCtrl": true,
    "showShareCtrl": false,
    "showZoomCtrl": true,
    "showFullscreenCtrl": true,
    "showAutoRotateCtrl": false,
    "sceneThumbsVertical": true,
    "sceneThumbsStatic": false,
    "title": true,
    "compass": false,
    "keyboardNav": false,
    "keyboardZoom": false,
    "sceneNextPrevLoop": false,
    "popover": true,
    "popoverPlacement": "top",
    "hotSpotBelowPopover": true,
    "popoverShowTrigger": "hover",
    "popoverHideTrigger": "leave",
    "mobile": false,
    "sceneId": "scene1",
    "sceneFadeDuration": 3000,
    "sceneBackgroundLoad": false,
    "scenes": {
        {{ scenes }}
    }
});