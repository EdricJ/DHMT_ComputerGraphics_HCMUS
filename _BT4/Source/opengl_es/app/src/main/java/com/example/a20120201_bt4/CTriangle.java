package com.example.a20120201_bt4;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.FloatBuffer;
import android.opengl.GLES20;

public class CTriangle {
    private final String vertexShaderCode =
            // This matrix member variable provides a hook to manipulate
            // the coordinates of the objects that use this vertex shader
            "uniform mat4 uMVPMatrix;" +
                    "attribute vec4 vPosition;" +
                    "void main() {" +
                    // the matrix must be included as a modifier of gl_Position
                    // Note that the uMVPMatrix factor *must be first* in order
                    // for the matrix multiplication product to be correct.
                    "  gl_Position = uMVPMatrix * vPosition;" +
                    "}";

    private final String fragment_shader_code =
            "precision mediump float;" +
                    "uniform vec4 vColor;" +
                    "void main() {" +
                    " gl_FragColor = vColor;" +
                    "}";

    private FloatBuffer vertex_buffer;
    private final int program;
    private int positionHandle;
    private int colorHandle;
    // Use to access and set the view transformation
    private int vPMatrixHandle;

    // number of coordinates per vertex in this array
    static final int COORS_PER_VERTEX = 3;
    // counterclockwise order
    static float[] triangle_coors = {
            0.0f, 0.622008459f, 0.0f, // top
            -0.5f, -0.311004243f, 0.0f, // bottom left
            0.5f, -0.311004243f, 0.0f // bottom right
    };

    // set color with red, green, blue and alpha (opacity) values
    float color[] = {0.3f, 0.5f, 0.7f, 1.0f};

    private final int vertexCount = triangle_coors.length / COORS_PER_VERTEX;
    // 4 bytes per vertex
    private final int vertexStride = COORS_PER_VERTEX * 4;

    // create a vertex shader type (GLES20.GL_VERTEX_SHADER)
    // or a fragment shader type (GLES20.GL_FRAGMENT_SHADER)
    public static int loadShader(int type, String shaderCode) {
        int shader = GLES20.glCreateShader(type);
        // add the shader 's source code and compile it
        GLES20.glShaderSource(shader, shaderCode);
        GLES20.glCompileShader(shader);
        return shader;
    }

    public CTriangle() {
        // initialize vertex byte buffer for shape coordinates
        // (number of coordinate values * 4 bytes per float)
        ByteBuffer bb = ByteBuffer.allocateDirect(triangle_coors.length * 4);
        // use the device hardware’s native byte order
        bb.order(ByteOrder.nativeOrder());
        // create a floating point buffer from the ByteBuffer
        vertex_buffer = bb.asFloatBuffer();
        // add the coordinates to the FloatBuffer
        vertex_buffer.put(triangle_coors);
        // set the buffer to read the first coordinate
        vertex_buffer.position(0);
        int vertexShader = MyGLRenderer.loadShader(GLES20.GL_VERTEX_SHADER,
                vertexShaderCode);
        int fragmentShader = MyGLRenderer.loadShader(GLES20.GL_FRAGMENT_SHADER,
                fragment_shader_code);

        // create empty OpenGL ES Program
        program = GLES20.glCreateProgram();
        // add the vertex shader to program
        GLES20.glAttachShader(program, vertexShader);
        // add the fragment shader to program
        GLES20.glAttachShader(program, fragmentShader);
        // creates OpenGL ES program executables
        GLES20.glLinkProgram(program);
    }

    public void draw(float[] mvpMatrix) {
        // add program to OpenGL ES environment
        GLES20.glUseProgram(program);
        // get handle to vertex shader’s vPosition member
        positionHandle = GLES20.glGetAttribLocation(program, "vPosition");
        // enable a handle to the triangle vertices
        GLES20.glEnableVertexAttribArray(positionHandle);
        // prepare the triangle coordinate data
        GLES20.glVertexAttribPointer(positionHandle, COORS_PER_VERTEX,
                GLES20.GL_FLOAT, false,
                vertexStride, vertex_buffer);
        // get handle to fragment shader’s vColor member
        colorHandle = GLES20.glGetUniformLocation(program, "vColor");
        // set color for drawing the triangle
        GLES20.glUniform4fv(colorHandle, 1, color, 0);

        // get handle to shape's transformation matrix
        vPMatrixHandle = GLES20.glGetUniformLocation(program, "uMVPMatrix");
        MyGLRenderer.checkGlError("glGetUniformLocation");

        // Pass the projection and view transformation to the shader
        GLES20.glUniformMatrix4fv(vPMatrixHandle, 1, false, mvpMatrix, 0);
        MyGLRenderer.checkGlError("glUniformMatrix4fv");

        // Draw the triangle
        GLES20.glDrawArrays(GLES20.GL_TRIANGLES, 0, vertexCount);

        // Disable vertex array
        GLES20.glDisableVertexAttribArray(positionHandle);
    }
}
