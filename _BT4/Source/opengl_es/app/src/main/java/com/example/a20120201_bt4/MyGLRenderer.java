package com.example.a20120201_bt4;
import javax.microedition.khronos.egl.EGLConfig;
import javax.microedition.khronos.opengles.GL10;

import android.opengl.GLES20;
import android.opengl.GLSurfaceView;
import android.opengl.Matrix;
import android.util.Log;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class MyGLRenderer implements GLSurfaceView.Renderer {
    private static final String TAG = "MyGLRenderer";
    private HashMap<String, CSquare> cSquare_map = new HashMap<>();

    private CTriangle cTriangle;
    private CSquare cSquare;
    private CCircle cCircle;
    private CLine cLine;

    // vPMatrix is an abbreviation for "Model View Projection Matrix"
    private final float[] vPMatrix = new float[16];
    private final float[] projectionMatrix = new float[16];
    private final float[] viewMatrix = new float[16];
    private float[] rotationMatrix = new float[16];
    public volatile float mAngle;

    private int screenWidth = 0;
    private int screenHeight = 0;
    private int square_number = 65;
    private final float[][] colors = {
            {0.29f, 0.57f, 1.0f, 1.0f},
            {0.8f, 0.0f, 0.0f, 1.0f},
            {0.13f, 0.8f, 0.0f, 1.0f},
            {1.0f, 0.84f, 0.0f, 1.0f}};


    public void onSurfaceCreated(GL10 unused, EGLConfig config) {
        // Set the background frame color
        GLES20.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        // initialize a triangle
        cTriangle = new CTriangle();
        // initialize a square
        String keyName = String.valueOf((char) this.square_number);
        cSquare = new CSquare(keyName, colors[this.square_number-65]);
        // initialize a circle
        cCircle = new CCircle();
        // initialize a line
        cLine = new CLine();

        //Adding the 4 squares to the grid and move them to their positions
        String square_key = "";
        square_key = addSquare();
        this.cSquare_map.get(square_key).moveSquare(0.5f, 0.5f);
        square_key = addSquare();
        this.cSquare_map.get(square_key).moveSquare(0.5f, -0.5f);
        square_key = addSquare();
        this.cSquare_map.get(square_key).moveSquare(-0.5f, 0.5f);
        square_key = addSquare();
        this.cSquare_map.get(square_key).moveSquare(-0.5f, -0.5f);
    }

    public void checkCollision(float touchX, float touchY) {
        //Step 1: normalize coordinates
        float[] touchClipMatrix = new float[]{
                2.0f * touchX / this.screenWidth - 1.0f,
                1.0f - touchY * 2 / this.screenHeight,
                0,
                1.0f
        };

        float[] invertedProjectionMatrix = new float[16];
        float[] invertedMViewMatrix = new float[16];
        Matrix.invertM(invertedProjectionMatrix,0, projectionMatrix, 0);
        Matrix.invertM(invertedMViewMatrix,0, viewMatrix, 0);

        //Calculation Matrices
        float[] unviewMatrix = new float[16];
        float[] mouse_worldspace = new float[4];

        //Getting mouse position in world space
        Matrix.multiplyMM(unviewMatrix, 0, invertedMViewMatrix, 0, invertedProjectionMatrix,0);
        Matrix.multiplyMV(mouse_worldspace, 0 , unviewMatrix, 0 , touchClipMatrix, 0);


        Log.i(TAG, "checkCollision-touchClipMatrix: "+ Arrays.toString(touchClipMatrix));
        Log.i(TAG, "checkCollision-invertedProjectionMatrix: "+ Arrays.toString(invertedProjectionMatrix));
        Log.i(TAG, "checkCollision-invertedMViewMatrix: "+ Arrays.toString(invertedMViewMatrix));
        Log.i(TAG, "checkCollision-mouse_worldspace: "+ Arrays.toString(mouse_worldspace));


        //Getting the camera position
        float [] cameraPosition = {0, 0, -6};

        //subtract camera position from the mouse_worldspace
        float [] ray_unnormalized = new float[4];
        for(int i = 0; i < 3; i++){
            ray_unnormalized[i] = mouse_worldspace[i] / mouse_worldspace[3] - cameraPosition[i];
        }

        //normalize ray_vector
        float ray_length = Matrix.length(ray_unnormalized[0], ray_unnormalized[1], ray_unnormalized[2]);
        float [] ray_vector = new float[4];
        for(int i=0; i<3; i++){
            ray_vector[i] = ray_unnormalized[i]/ray_length;
        }
        Log.i(TAG, "checkCollision - ray_vector: "+ Arrays.toString(ray_vector));
        /*
        LinePlaneIntersection linePlaneIntersection = new LinePlaneIntersection();
        LinePlaneIntersection.Vector3D rv = new LinePlaneIntersection.Vector3D(ray_vector[0], ray_vector[1], ray_vector[2]);
        LinePlaneIntersection.Vector3D rp = new LinePlaneIntersection.Vector3D(mouse_worldspace[0], mouse_worldspace[1], mouse_worldspace[2]);
        LinePlaneIntersection.Vector3D pn = new LinePlaneIntersection.Vector3D(0.0, 0.0, 0.0);
        LinePlaneIntersection.Vector3D pp = new LinePlaneIntersection.Vector3D(0.0, 0.0, 1.0);
        LinePlaneIntersection.Vector3D ip = linePlaneIntersection.intersectPoint(rv, rp, pn, pp);
        Log.i(TAG, "checkCollision-intersection point: "+ip);*/
    }

    public String addSquare() {
        String keyName = String.valueOf((char) this.square_number);
        this.cSquare_map.put(keyName, new CSquare(keyName, colors[this.square_number-65]));
        this.square_number += 1;
        return keyName;
    }

    public void logMatrices() {
        Log.i(TAG, "MVPMatrice: " + Arrays.toString(this.vPMatrix));
        Log.i(TAG, "mProjectionMarice: " + Arrays.toString(this.projectionMatrix));
        Log.i(TAG, "mViewMatrice: " + Arrays.toString(this.viewMatrix));
    }

    public void onDrawFrame(GL10 unused) {
        float[] scratch = new float[16];
        // Redraw background color
        GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT);

        // Set the camera position (View matrix)
        Matrix.setLookAtM(viewMatrix, 0, 0, 0, 3, 0f, 0f, 0f, 0f, 1.0f, 0.0f);

        //translate the Matrix
        //Matrix.translateM(viewMatrix, 0, 2f, 1f, 0);

        // Calculate the projection and view transformation
        Matrix.multiplyMM(vPMatrix, 0, projectionMatrix, 0, viewMatrix, 0);

        // Draw square
        //cSquare.draw(vPMatrix);

        //Draw cirle
        //cCircle.draw(vPMatrix);

        //Draw line
        //cLine.draw(vPMatrix);

        // Create a rotation for the square
        Matrix.setRotateM(rotationMatrix, 0, mAngle, 0, 0.0f, 1.0f);
        // Combine the rotation matrix with the projection and camera view
        // Note that the mMVPMatrix factor *must be first* in order
        // for the matrix multiplication product to be correct.
        //Matrix.multiplyMM(scratch, 0, vPMatrix, 0, rotationMatrix, 0);

        // Draw multiple squares
        /*for (Map.Entry<String, CSquare> s : this.cSquare_map.entrySet()) {
            s.getValue().draw(scratch);
        }*/

        // Create a rotation transformation for the triangle
        //long time = SystemClock.uptimeMillis() % 4000L;
        //float angle = 0.090f * ((int) time);
        Matrix.setRotateM(rotationMatrix, 0, mAngle, 0, 0, -1.0f);

        // Combine the rotation matrix with the projection and camera view
        // Note that the vPMatrix factor *must be first* in order
        // for the matrix multiplication product to be correct.
        Matrix.multiplyMM(scratch, 0, vPMatrix, 0, rotationMatrix, 0);

        //draw triagle and rotate it
        cTriangle.draw(scratch);
    }

    public void onSurfaceChanged(GL10 unused, int width, int height) {
        this.screenWidth = width;
        this.screenHeight = height;

        // set the viewport to the size of the view.
        GLES20.glViewport(0, 0, width, height);

        float ratio = (float) width / height;

        // this projection matrix is applied to object coordinates
        // in the onDrawFrame() method
        Matrix.frustumM(projectionMatrix, 0, -ratio, ratio, -1, 1, 3, 7);
    }

    // create a vertex shader type (GLES20.GL_VERTEX_SHADER)
    // or a fragment shader type (GLES20.GL_FRAGMENT_SHADER)
    public static int loadShader(int type, String shaderCode) {
        int shader = GLES20.glCreateShader(type);
        GLES20.glShaderSource(shader, shaderCode);
        GLES20.glCompileShader(shader);
        return shader;
    }

    /**
     * Utility method for debugging OpenGL calls. Provide the name of the call
     * just after making it:
     *
     * <pre>
     * mColorHandle = GLES20.glGetUniformLocation(mProgram, "vColor");
     * MyGLRenderer.checkGlError("glGetUniformLocation");</pre>
     *
     * If the operation is not successful, the check throws an error.
     *
     * @param glOperation - Name of the OpenGL call to check.
     */
    public static void checkGlError(String glOperation) {
        int error;
        while ((error = GLES20.glGetError()) != GLES20.GL_NO_ERROR) {
            Log.e(TAG, glOperation + ": glError " + error);
            throw new RuntimeException(glOperation + ": glError " + error);
        }
    }

    /**
     * Returns the rotation angle of the triangle shape (mTriangle).
     *
     * @return - A float representing the rotation angle.
     */
    public float getAngle() {
        return mAngle;
    }
    /**
     * Sets the rotation angle of the triangle shape (mTriangle).
     */
    public void setAngle(float angle) {
        mAngle = angle;
    }
}
