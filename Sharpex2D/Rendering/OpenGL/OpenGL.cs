// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class OpenGL
    {
        public delegate IntPtr wglCreateContextAttribsARB(IntPtr hDC, IntPtr hShareContext, int[] attribList);

        public const uint GL_VERSION_1_1 = 1u;
        public const uint GL_ACCUM = 256u;
        public const uint GL_LOAD = 257u;
        public const uint GL_RETURN = 258u;
        public const uint GL_MULT = 259u;
        public const uint GL_ADD = 260u;
        public const uint GL_NEVER = 512u;
        public const uint GL_LESS = 513u;
        public const uint GL_EQUAL = 514u;
        public const uint GL_LEQUAL = 515u;
        public const uint GL_GREATER = 516u;
        public const uint GL_NOTEQUAL = 517u;
        public const uint GL_GEQUAL = 518u;
        public const uint GL_ALWAYS = 519u;
        public const uint GL_CURRENT_BIT = 1u;
        public const uint GL_POINT_BIT = 2u;
        public const uint GL_LINE_BIT = 4u;
        public const uint GL_POLYGON_BIT = 8u;
        public const uint GL_POLYGON_STIPPLE_BIT = 16u;
        public const uint GL_PIXEL_MODE_BIT = 32u;
        public const uint GL_LIGHTING_BIT = 64u;
        public const uint GL_FOG_BIT = 128u;
        public const uint GL_DEPTH_BUFFER_BIT = 256u;
        public const uint GL_ACCUM_BUFFER_BIT = 512u;
        public const uint GL_STENCIL_BUFFER_BIT = 1024u;
        public const uint GL_VIEWPORT_BIT = 2048u;
        public const uint GL_TRANSFORM_BIT = 4096u;
        public const uint GL_ENABLE_BIT = 8192u;
        public const uint GL_COLOR_BUFFER_BIT = 16384u;
        public const uint GL_HINT_BIT = 32768u;
        public const uint GL_EVAL_BIT = 65536u;
        public const uint GL_LIST_BIT = 131072u;
        public const uint GL_TEXTURE_BIT = 262144u;
        public const uint GL_SCISSOR_BIT = 524288u;
        public const uint GL_ALL_ATTRIB_BITS = 1048575u;

        /// <summary>
        /// Treats each vertex as a single point. Vertex n defines point n. N points are drawn.
        /// </summary>
        public const uint GL_POINTS = 0u;

        /// <summary>
        /// Treats each pair of vertices as an independent line segment. Vertices 2n - 1 and 2n define line n. N/2 lines are drawn.
        /// </summary>
        public const uint GL_LINES = 1u;

        /// <summary>
        /// Draws a connected group of line segments from the first vertex to the last, then back to the first. Vertices n and n + 1 define line n. The last line, however, is defined by vertices N and 1. N lines are drawn.
        /// </summary>
        public const uint GL_LINE_LOOP = 2u;

        /// <summary>
        /// Draws a connected group of line segments from the first vertex to the last. Vertices n and n+1 define line n. N - 1 lines are drawn.
        /// </summary>
        public const uint GL_LINE_STRIP = 3u;

        /// <summary>
        /// Treats each triplet of vertices as an independent triangle. Vertices 3n - 2, 3n - 1, and 3n define triangle n. N/3 triangles are drawn.
        /// </summary>
        public const uint GL_TRIANGLES = 4u;

        /// <summary>
        /// Draws a connected group of triangles. One triangle is defined for each vertex presented after the first two vertices. For odd n, vertices n, n + 1, and n + 2 define triangle n. For even n, vertices n + 1, n, and n + 2 define triangle n. N - 2 triangles are drawn.
        /// </summary>
        public const uint GL_TRIANGLE_STRIP = 5u;

        /// <summary>
        /// Draws a connected group of triangles. one triangle is defined for each vertex presented after the first two vertices. Vertices 1, n + 1, n + 2 define triangle n. N - 2 triangles are drawn.
        /// </summary>
        public const uint GL_TRIANGLE_FAN = 6u;

        /// <summary>
        /// Treats each group of four vertices as an independent quadrilateral. Vertices 4n - 3, 4n - 2, 4n - 1, and 4n define quadrilateral n. N/4 quadrilaterals are drawn.
        /// </summary>
        public const uint GL_QUADS = 7u;

        /// <summary>
        /// Draws a connected group of quadrilaterals. One quadrilateral is defined for each pair of vertices presented after the first pair. Vertices 2n - 1, 2n, 2n + 2, and 2n + 1 define quadrilateral n. N/2 - 1 quadrilaterals are drawn. Note that the order in which vertices are used to construct a quadrilateral from strip data is different from that used with independent data.
        /// </summary>
        public const uint GL_QUAD_STRIP = 8u;

        /// <summary>
        /// Draws a single, convex polygon. Vertices 1 through N define this polygon.
        /// </summary>
        public const uint GL_POLYGON = 9u;

        public const uint GL_ZERO = 0u;
        public const uint GL_ONE = 1u;
        public const uint GL_SRC_COLOR = 768u;
        public const uint GL_ONE_MINUS_SRC_COLOR = 769u;
        public const uint GL_SRC_ALPHA = 770u;
        public const uint GL_ONE_MINUS_SRC_ALPHA = 771u;
        public const uint GL_DST_ALPHA = 772u;
        public const uint GL_ONE_MINUS_DST_ALPHA = 773u;
        public const uint GL_DST_COLOR = 774u;
        public const uint GL_ONE_MINUS_DST_COLOR = 775u;
        public const uint GL_SRC_ALPHA_SATURATE = 776u;
        public const uint GL_TRUE = 1u;
        public const uint GL_FALSE = 0u;
        public const uint GL_CLIP_PLANE0 = 12288u;
        public const uint GL_CLIP_PLANE1 = 12289u;
        public const uint GL_CLIP_PLANE2 = 12290u;
        public const uint GL_CLIP_PLANE3 = 12291u;
        public const uint GL_CLIP_PLANE4 = 12292u;
        public const uint GL_CLIP_PLANE5 = 12293u;
        public const uint GL_BYTE = 5120u;
        public const uint GL_UNSIGNED_BYTE = 5121u;
        public const uint GL_SHORT = 5122u;
        public const uint GL_UNSIGNED_SHORT = 5123u;
        public const uint GL_INT = 5124u;
        public const uint GL_UNSIGNED_INT = 5125u;
        public const uint GL_FLOAT = 5126u;
        public const uint GL_2_BYTES = 5127u;
        public const uint GL_3_BYTES = 5128u;
        public const uint GL_4_BYTES = 5129u;
        public const uint GL_DOUBLE = 5130u;
        public const uint GL_NONE = 0u;
        public const uint GL_FRONT_LEFT = 1024u;
        public const uint GL_FRONT_RIGHT = 1025u;
        public const uint GL_BACK_LEFT = 1026u;
        public const uint GL_BACK_RIGHT = 1027u;
        public const uint GL_FRONT = 1028u;
        public const uint GL_BACK = 1029u;
        public const uint GL_LEFT = 1030u;
        public const uint GL_RIGHT = 1031u;
        public const uint GL_FRONT_AND_BACK = 1032u;
        public const uint GL_AUX0 = 1033u;
        public const uint GL_AUX1 = 1034u;
        public const uint GL_AUX2 = 1035u;
        public const uint GL_AUX3 = 1036u;
        public const uint GL_NO_ERROR = 0u;
        public const uint GL_INVALID_ENUM = 1280u;
        public const uint GL_INVALID_VALUE = 1281u;
        public const uint GL_INVALID_OPERATION = 1282u;
        public const uint GL_STACK_OVERFLOW = 1283u;
        public const uint GL_STACK_UNDERFLOW = 1284u;
        public const uint GL_OUT_OF_MEMORY = 1285u;
        public const uint GL_2D = 1536u;
        public const uint GL_3D = 1537u;
        public const uint GL_4D_COLOR = 1538u;
        public const uint GL_3D_COLOR_TEXTURE = 1539u;
        public const uint GL_4D_COLOR_TEXTURE = 1540u;
        public const uint GL_PASS_THROUGH_TOKEN = 1792u;
        public const uint GL_POINT_TOKEN = 1793u;
        public const uint GL_LINE_TOKEN = 1794u;
        public const uint GL_POLYGON_TOKEN = 1795u;
        public const uint GL_BITMAP_TOKEN = 1796u;
        public const uint GL_DRAW_PIXEL_TOKEN = 1797u;
        public const uint GL_COPY_PIXEL_TOKEN = 1798u;
        public const uint GL_LINE_RESET_TOKEN = 1799u;
        public const uint GL_EXP = 2048u;
        public const uint GL_EXP2 = 2049u;
        public const uint GL_CW = 2304u;
        public const uint GL_CCW = 2305u;
        public const uint GL_COEFF = 2560u;
        public const uint GL_ORDER = 2561u;
        public const uint GL_DOMAIN = 2562u;
        public const uint GL_CURRENT_COLOR = 2816u;
        public const uint GL_CURRENT_INDEX = 2817u;
        public const uint GL_CURRENT_NORMAL = 2818u;
        public const uint GL_CURRENT_TEXTURE_COORDS = 2819u;
        public const uint GL_CURRENT_RASTER_COLOR = 2820u;
        public const uint GL_CURRENT_RASTER_INDEX = 2821u;
        public const uint GL_CURRENT_RASTER_TEXTURE_COORDS = 2822u;
        public const uint GL_CURRENT_RASTER_POSITION = 2823u;
        public const uint GL_CURRENT_RASTER_POSITION_VALID = 2824u;
        public const uint GL_CURRENT_RASTER_DISTANCE = 2825u;
        public const uint GL_POINT_SMOOTH = 2832u;
        public const uint GL_POINT_SIZE = 2833u;
        public const uint GL_POINT_SIZE_RANGE = 2834u;
        public const uint GL_POINT_SIZE_GRANULARITY = 2835u;
        public const uint GL_LINE_SMOOTH = 2848u;
        public const uint GL_LINE_WIDTH = 2849u;
        public const uint GL_LINE_WIDTH_RANGE = 2850u;
        public const uint GL_LINE_WIDTH_GRANULARITY = 2851u;
        public const uint GL_LINE_STIPPLE = 2852u;
        public const uint GL_LINE_STIPPLE_PATTERN = 2853u;
        public const uint GL_LINE_STIPPLE_REPEAT = 2854u;
        public const uint GL_LIST_MODE = 2864u;
        public const uint GL_MAX_LIST_NESTING = 2865u;
        public const uint GL_LIST_BASE = 2866u;
        public const uint GL_LIST_INDEX = 2867u;
        public const uint GL_POLYGON_MODE = 2880u;
        public const uint GL_POLYGON_SMOOTH = 2881u;
        public const uint GL_POLYGON_STIPPLE = 2882u;
        public const uint GL_EDGE_FLAG = 2883u;
        public const uint GL_CULL_FACE = 2884u;
        public const uint GL_CULL_FACE_MODE = 2885u;
        public const uint GL_FRONT_FACE = 2886u;
        public const uint GL_LIGHTING = 2896u;
        public const uint GL_LIGHT_MODEL_LOCAL_VIEWER = 2897u;
        public const uint GL_LIGHT_MODEL_TWO_SIDE = 2898u;
        public const uint GL_LIGHT_MODEL_AMBIENT = 2899u;
        public const uint GL_SHADE_MODEL = 2900u;
        public const uint GL_COLOR_MATERIAL_FACE = 2901u;
        public const uint GL_COLOR_MATERIAL_PARAMETER = 2902u;
        public const uint GL_COLOR_MATERIAL = 2903u;
        public const uint GL_FOG = 2912u;
        public const uint GL_FOG_INDEX = 2913u;
        public const uint GL_FOG_DENSITY = 2914u;
        public const uint GL_FOG_START = 2915u;
        public const uint GL_FOG_END = 2916u;
        public const uint GL_FOG_MODE = 2917u;
        public const uint GL_FOG_COLOR = 2918u;
        public const uint GL_DEPTH_RANGE = 2928u;
        public const uint GL_DEPTH_TEST = 2929u;
        public const uint GL_DEPTH_WRITEMASK = 2930u;
        public const uint GL_DEPTH_CLEAR_VALUE = 2931u;
        public const uint GL_DEPTH_FUNC = 2932u;
        public const uint GL_ACCUM_CLEAR_VALUE = 2944u;
        public const uint GL_STENCIL_TEST = 2960u;
        public const uint GL_STENCIL_CLEAR_VALUE = 2961u;
        public const uint GL_STENCIL_FUNC = 2962u;
        public const uint GL_STENCIL_VALUE_MASK = 2963u;
        public const uint GL_STENCIL_FAIL = 2964u;
        public const uint GL_STENCIL_PASS_DEPTH_FAIL = 2965u;
        public const uint GL_STENCIL_PASS_DEPTH_PASS = 2966u;
        public const uint GL_STENCIL_REF = 2967u;
        public const uint GL_STENCIL_WRITEMASK = 2968u;
        public const uint GL_MATRIX_MODE = 2976u;
        public const uint GL_NORMALIZE = 2977u;
        public const uint GL_VIEWPORT = 2978u;
        public const uint GL_MODELVIEW_STACK_DEPTH = 2979u;
        public const uint GL_PROJECTION_STACK_DEPTH = 2980u;
        public const uint GL_TEXTURE_STACK_DEPTH = 2981u;
        public const uint GL_MODELVIEW_MATRIX = 2982u;
        public const uint GL_PROJECTION_MATRIX = 2983u;
        public const uint GL_TEXTURE_MATRIX = 2984u;
        public const uint GL_ATTRIB_STACK_DEPTH = 2992u;
        public const uint GL_CLIENT_ATTRIB_STACK_DEPTH = 2993u;
        public const uint GL_ALPHA_TEST = 3008u;
        public const uint GL_ALPHA_TEST_FUNC = 3009u;
        public const uint GL_ALPHA_TEST_REF = 3010u;
        public const uint GL_DITHER = 3024u;
        public const uint GL_BLEND_DST = 3040u;
        public const uint GL_BLEND_SRC = 3041u;
        public const uint GL_BLEND = 3042u;
        public const uint GL_LOGIC_OP_MODE = 3056u;
        public const uint GL_INDEX_LOGIC_OP = 3057u;
        public const uint GL_COLOR_LOGIC_OP = 3058u;
        public const uint GL_AUX_BUFFERS = 3072u;
        public const uint GL_DRAW_BUFFER = 3073u;
        public const uint GL_READ_BUFFER = 3074u;
        public const uint GL_SCISSOR_BOX = 3088u;
        public const uint GL_SCISSOR_TEST = 3089u;
        public const uint GL_INDEX_CLEAR_VALUE = 3104u;
        public const uint GL_INDEX_WRITEMASK = 3105u;
        public const uint GL_COLOR_CLEAR_VALUE = 3106u;
        public const uint GL_COLOR_WRITEMASK = 3107u;
        public const uint GL_INDEX_MODE = 3120u;
        public const uint GL_RGBA_MODE = 3121u;
        public const uint GL_DOUBLEBUFFER = 3122u;
        public const uint GL_STEREO = 3123u;
        public const uint GL_RENDER_MODE = 3136u;
        public const uint GL_PERSPECTIVE_CORRECTION_HINT = 3152u;
        public const uint GL_POINT_SMOOTH_HINT = 3153u;
        public const uint GL_LINE_SMOOTH_HINT = 3154u;
        public const uint GL_POLYGON_SMOOTH_HINT = 3155u;
        public const uint GL_FOG_HINT = 3156u;
        public const uint GL_TEXTURE_GEN_S = 3168u;
        public const uint GL_TEXTURE_GEN_T = 3169u;
        public const uint GL_TEXTURE_GEN_R = 3170u;
        public const uint GL_TEXTURE_GEN_Q = 3171u;
        public const uint GL_PIXEL_MAP_I_TO_I = 3184u;
        public const uint GL_PIXEL_MAP_S_TO_S = 3185u;
        public const uint GL_PIXEL_MAP_I_TO_R = 3186u;
        public const uint GL_PIXEL_MAP_I_TO_G = 3187u;
        public const uint GL_PIXEL_MAP_I_TO_B = 3188u;
        public const uint GL_PIXEL_MAP_I_TO_A = 3189u;
        public const uint GL_PIXEL_MAP_R_TO_R = 3190u;
        public const uint GL_PIXEL_MAP_G_TO_G = 3191u;
        public const uint GL_PIXEL_MAP_B_TO_B = 3192u;
        public const uint GL_PIXEL_MAP_A_TO_A = 3193u;
        public const uint GL_PIXEL_MAP_I_TO_I_SIZE = 3248u;
        public const uint GL_PIXEL_MAP_S_TO_S_SIZE = 3249u;
        public const uint GL_PIXEL_MAP_I_TO_R_SIZE = 3250u;
        public const uint GL_PIXEL_MAP_I_TO_G_SIZE = 3251u;
        public const uint GL_PIXEL_MAP_I_TO_B_SIZE = 3252u;
        public const uint GL_PIXEL_MAP_I_TO_A_SIZE = 3253u;
        public const uint GL_PIXEL_MAP_R_TO_R_SIZE = 3254u;
        public const uint GL_PIXEL_MAP_G_TO_G_SIZE = 3255u;
        public const uint GL_PIXEL_MAP_B_TO_B_SIZE = 3256u;
        public const uint GL_PIXEL_MAP_A_TO_A_SIZE = 3257u;
        public const uint GL_UNPACK_SWAP_BYTES = 3312u;
        public const uint GL_UNPACK_LSB_FIRST = 3313u;
        public const uint GL_UNPACK_ROW_LENGTH = 3314u;
        public const uint GL_UNPACK_SKIP_ROWS = 3315u;
        public const uint GL_UNPACK_SKIP_PIXELS = 3316u;
        public const uint GL_UNPACK_ALIGNMENT = 3317u;
        public const uint GL_PACK_SWAP_BYTES = 3328u;
        public const uint GL_PACK_LSB_FIRST = 3329u;
        public const uint GL_PACK_ROW_LENGTH = 3330u;
        public const uint GL_PACK_SKIP_ROWS = 3331u;
        public const uint GL_PACK_SKIP_PIXELS = 3332u;
        public const uint GL_PACK_ALIGNMENT = 3333u;
        public const uint GL_MAP_COLOR = 3344u;
        public const uint GL_MAP_STENCIL = 3345u;
        public const uint GL_INDEX_SHIFT = 3346u;
        public const uint GL_INDEX_OFFSET = 3347u;
        public const uint GL_RED_SCALE = 3348u;
        public const uint GL_RED_BIAS = 3349u;
        public const uint GL_ZOOM_X = 3350u;
        public const uint GL_ZOOM_Y = 3351u;
        public const uint GL_GREEN_SCALE = 3352u;
        public const uint GL_GREEN_BIAS = 3353u;
        public const uint GL_BLUE_SCALE = 3354u;
        public const uint GL_BLUE_BIAS = 3355u;
        public const uint GL_ALPHA_SCALE = 3356u;
        public const uint GL_ALPHA_BIAS = 3357u;
        public const uint GL_DEPTH_SCALE = 3358u;
        public const uint GL_DEPTH_BIAS = 3359u;
        public const uint GL_MAX_EVAL_ORDER = 3376u;
        public const uint GL_MAX_LIGHTS = 3377u;
        public const uint GL_MAX_CLIP_PLANES = 3378u;
        public const uint GL_MAX_TEXTURE_SIZE = 3379u;
        public const uint GL_MAX_PIXEL_MAP_TABLE = 3380u;
        public const uint GL_MAX_ATTRIB_STACK_DEPTH = 3381u;
        public const uint GL_MAX_MODELVIEW_STACK_DEPTH = 3382u;
        public const uint GL_MAX_NAME_STACK_DEPTH = 3383u;
        public const uint GL_MAX_PROJECTION_STACK_DEPTH = 3384u;
        public const uint GL_MAX_TEXTURE_STACK_DEPTH = 3385u;
        public const uint GL_MAX_VIEWPORT_DIMS = 3386u;
        public const uint GL_MAX_CLIENT_ATTRIB_STACK_DEPTH = 3387u;
        public const uint GL_SUBPIXEL_BITS = 3408u;
        public const uint GL_INDEX_BITS = 3409u;
        public const uint GL_RED_BITS = 3410u;
        public const uint GL_GREEN_BITS = 3411u;
        public const uint GL_BLUE_BITS = 3412u;
        public const uint GL_ALPHA_BITS = 3413u;
        public const uint GL_DEPTH_BITS = 3414u;
        public const uint GL_STENCIL_BITS = 3415u;
        public const uint GL_ACCUM_RED_BITS = 3416u;
        public const uint GL_ACCUM_GREEN_BITS = 3417u;
        public const uint GL_ACCUM_BLUE_BITS = 3418u;
        public const uint GL_ACCUM_ALPHA_BITS = 3419u;
        public const uint GL_NAME_STACK_DEPTH = 3440u;
        public const uint GL_AUTO_NORMAL = 3456u;
        public const uint GL_MAP1_COLOR_4 = 3472u;
        public const uint GL_MAP1_INDEX = 3473u;
        public const uint GL_MAP1_NORMAL = 3474u;
        public const uint GL_MAP1_TEXTURE_COORD_1 = 3475u;
        public const uint GL_MAP1_TEXTURE_COORD_2 = 3476u;
        public const uint GL_MAP1_TEXTURE_COORD_3 = 3477u;
        public const uint GL_MAP1_TEXTURE_COORD_4 = 3478u;
        public const uint GL_MAP1_VERTEX_3 = 3479u;
        public const uint GL_MAP1_VERTEX_4 = 3480u;
        public const uint GL_MAP2_COLOR_4 = 3504u;
        public const uint GL_MAP2_INDEX = 3505u;
        public const uint GL_MAP2_NORMAL = 3506u;
        public const uint GL_MAP2_TEXTURE_COORD_1 = 3507u;
        public const uint GL_MAP2_TEXTURE_COORD_2 = 3508u;
        public const uint GL_MAP2_TEXTURE_COORD_3 = 3509u;
        public const uint GL_MAP2_TEXTURE_COORD_4 = 3510u;
        public const uint GL_MAP2_VERTEX_3 = 3511u;
        public const uint GL_MAP2_VERTEX_4 = 3512u;
        public const uint GL_MAP1_GRID_DOMAIN = 3536u;
        public const uint GL_MAP1_GRID_SEGMENTS = 3537u;
        public const uint GL_MAP2_GRID_DOMAIN = 3538u;
        public const uint GL_MAP2_GRID_SEGMENTS = 3539u;
        public const uint GL_TEXTURE_1D = 3552u;
        public const uint GL_TEXTURE_2D = 3553u;
        public const uint GL_FEEDBACK_BUFFER_POINTER = 3568u;
        public const uint GL_FEEDBACK_BUFFER_SIZE = 3569u;
        public const uint GL_FEEDBACK_BUFFER_TYPE = 3570u;
        public const uint GL_SELECTION_BUFFER_POINTER = 3571u;
        public const uint GL_SELECTION_BUFFER_SIZE = 3572u;
        public const uint GL_TEXTURE_WIDTH = 4096u;
        public const uint GL_TEXTURE_HEIGHT = 4097u;
        public const uint GL_TEXTURE_INTERNAL_FORMAT = 4099u;
        public const uint GL_TEXTURE_BORDER_COLOR = 4100u;
        public const uint GL_TEXTURE_BORDER = 4101u;
        public const uint GL_DONT_CARE = 4352u;
        public const uint GL_FASTEST = 4353u;
        public const uint GL_NICEST = 4354u;
        public const uint GL_LIGHT0 = 16384u;
        public const uint GL_LIGHT1 = 16385u;
        public const uint GL_LIGHT2 = 16386u;
        public const uint GL_LIGHT3 = 16387u;
        public const uint GL_LIGHT4 = 16388u;
        public const uint GL_LIGHT5 = 16389u;
        public const uint GL_LIGHT6 = 16390u;
        public const uint GL_LIGHT7 = 16391u;
        public const uint GL_AMBIENT = 4608u;
        public const uint GL_DIFFUSE = 4609u;
        public const uint GL_SPECULAR = 4610u;
        public const uint GL_POSITION = 4611u;
        public const uint GL_SPOT_DIRECTION = 4612u;
        public const uint GL_SPOT_EXPONENT = 4613u;
        public const uint GL_SPOT_CUTOFF = 4614u;
        public const uint GL_CONSTANT_ATTENUATION = 4615u;
        public const uint GL_LINEAR_ATTENUATION = 4616u;
        public const uint GL_QUADRATIC_ATTENUATION = 4617u;
        public const uint GL_COMPILE = 4864u;
        public const uint GL_COMPILE_AND_EXECUTE = 4865u;
        public const uint GL_CLEAR = 5376u;
        public const uint GL_AND = 5377u;
        public const uint GL_AND_REVERSE = 5378u;
        public const uint GL_COPY = 5379u;
        public const uint GL_AND_INVERTED = 5380u;
        public const uint GL_NOOP = 5381u;
        public const uint GL_XOR = 5382u;
        public const uint GL_OR = 5383u;
        public const uint GL_NOR = 5384u;
        public const uint GL_EQUIV = 5385u;
        public const uint GL_INVERT = 5386u;
        public const uint GL_OR_REVERSE = 5387u;
        public const uint GL_COPY_INVERTED = 5388u;
        public const uint GL_OR_INVERTED = 5389u;
        public const uint GL_NAND = 5390u;
        public const uint GL_SET = 5391u;
        public const uint GL_EMISSION = 5632u;
        public const uint GL_SHININESS = 5633u;
        public const uint GL_AMBIENT_AND_DIFFUSE = 5634u;
        public const uint GL_COLOR_INDEXES = 5635u;
        public const uint GL_MODELVIEW = 5888u;
        public const uint GL_PROJECTION = 5889u;
        public const uint GL_TEXTURE = 5890u;
        public const uint GL_COLOR = 6144u;
        public const uint GL_DEPTH = 6145u;
        public const uint GL_STENCIL = 6146u;
        public const uint GL_COLOR_INDEX = 6400u;
        public const uint GL_STENCIL_INDEX = 6401u;
        public const uint GL_DEPTH_COMPONENT = 6402u;
        public const uint GL_RED = 6403u;
        public const uint GL_GREEN = 6404u;
        public const uint GL_BLUE = 6405u;
        public const uint GL_ALPHA = 6406u;
        public const uint GL_RGB = 6407u;
        public const uint GL_RGBA = 6408u;
        public const uint GL_LUMINANCE = 6409u;
        public const uint GL_LUMINANCE_ALPHA = 6410u;
        public const uint GL_BITMAP = 6656u;
        public const uint GL_POINT = 6912u;
        public const uint GL_LINE = 6913u;
        public const uint GL_FILL = 6914u;
        public const uint GL_RENDER = 7168u;
        public const uint GL_FEEDBACK = 7169u;
        public const uint GL_SELECT = 7170u;
        public const uint GL_FLAT = 7424u;
        public const uint GL_SMOOTH = 7425u;
        public const uint GL_KEEP = 7680u;
        public const uint GL_REPLACE = 7681u;
        public const uint GL_INCR = 7682u;
        public const uint GL_DECR = 7683u;
        public const uint GL_VENDOR = 7936u;
        public const uint GL_RENDERER = 7937u;
        public const uint GL_VERSION = 7938u;
        public const uint GL_EXTENSIONS = 7939u;
        public const uint GL_S = 8192u;
        public const uint GL_T = 8193u;
        public const uint GL_R = 8194u;
        public const uint GL_Q = 8195u;
        public const uint GL_MODULATE = 8448u;
        public const uint GL_DECAL = 8449u;
        public const uint GL_TEXTURE_ENV_MODE = 8704u;
        public const uint GL_TEXTURE_ENV_COLOR = 8705u;
        public const uint GL_TEXTURE_ENV = 8960u;
        public const uint GL_EYE_LINEAR = 9216u;
        public const uint GL_OBJECT_LINEAR = 9217u;
        public const uint GL_SPHERE_MAP = 9218u;
        public const uint GL_TEXTURE_GEN_MODE = 9472u;
        public const uint GL_OBJECT_PLANE = 9473u;
        public const uint GL_EYE_PLANE = 9474u;
        public const uint GL_NEAREST = 9728u;
        public const uint GL_LINEAR = 9729u;
        public const uint GL_NEAREST_MIPMAP_NEAREST = 9984u;
        public const uint GL_LINEAR_MIPMAP_NEAREST = 9985u;
        public const uint GL_NEAREST_MIPMAP_LINEAR = 9986u;
        public const uint GL_LINEAR_MIPMAP_LINEAR = 9987u;
        public const uint GL_TEXTURE_MAG_FILTER = 10240u;
        public const uint GL_TEXTURE_MIN_FILTER = 10241u;
        public const uint GL_TEXTURE_WRAP_S = 10242u;
        public const uint GL_TEXTURE_WRAP_T = 10243u;
        public const uint GL_CLAMP = 10496u;
        public const uint GL_REPEAT = 10497u;
        public const uint GL_CLIENT_PIXEL_STORE_BIT = 1u;
        public const uint GL_CLIENT_VERTEX_ARRAY_BIT = 2u;
        public const uint GL_CLIENT_ALL_ATTRIB_BITS = 4294967295u;
        public const uint GL_POLYGON_OFFSET_FACTOR = 32824u;
        public const uint GL_POLYGON_OFFSET_UNITS = 10752u;
        public const uint GL_POLYGON_OFFSET_POINT = 10753u;
        public const uint GL_POLYGON_OFFSET_LINE = 10754u;
        public const uint GL_POLYGON_OFFSET_FILL = 32823u;
        public const uint GL_ALPHA4 = 32827u;
        public const uint GL_ALPHA8 = 32828u;
        public const uint GL_ALPHA12 = 32829u;
        public const uint GL_ALPHA16 = 32830u;
        public const uint GL_LUMINANCE4 = 32831u;
        public const uint GL_LUMINANCE8 = 32832u;
        public const uint GL_LUMINANCE12 = 32833u;
        public const uint GL_LUMINANCE16 = 32834u;
        public const uint GL_LUMINANCE4_ALPHA4 = 32835u;
        public const uint GL_LUMINANCE6_ALPHA2 = 32836u;
        public const uint GL_LUMINANCE8_ALPHA8 = 32837u;
        public const uint GL_LUMINANCE12_ALPHA4 = 32838u;
        public const uint GL_LUMINANCE12_ALPHA12 = 32839u;
        public const uint GL_LUMINANCE16_ALPHA16 = 32840u;
        public const uint GL_INTENSITY = 32841u;
        public const uint GL_INTENSITY4 = 32842u;
        public const uint GL_INTENSITY8 = 32843u;
        public const uint GL_INTENSITY12 = 32844u;
        public const uint GL_INTENSITY16 = 32845u;
        public const uint GL_R3_G3_B2 = 10768u;
        public const uint GL_RGB4 = 32847u;
        public const uint GL_RGB5 = 32848u;
        public const uint GL_RGB8 = 32849u;
        public const uint GL_RGB10 = 32850u;
        public const uint GL_RGB12 = 32851u;
        public const uint GL_RGB16 = 32852u;
        public const uint GL_RGBA2 = 32853u;
        public const uint GL_RGBA4 = 32854u;
        public const uint GL_RGB5_A1 = 32855u;
        public const uint GL_RGBA8 = 32856u;
        public const uint GL_RGB10_A2 = 32857u;
        public const uint GL_RGBA12 = 32858u;
        public const uint GL_RGBA16 = 32859u;
        public const uint GL_TEXTURE_RED_SIZE = 32860u;
        public const uint GL_TEXTURE_GREEN_SIZE = 32861u;
        public const uint GL_TEXTURE_BLUE_SIZE = 32862u;
        public const uint GL_TEXTURE_ALPHA_SIZE = 32863u;
        public const uint GL_TEXTURE_LUMINANCE_SIZE = 32864u;
        public const uint GL_TEXTURE_INTENSITY_SIZE = 32865u;
        public const uint GL_PROXY_TEXTURE_1D = 32867u;
        public const uint GL_PROXY_TEXTURE_2D = 32868u;
        public const uint GL_TEXTURE_PRIORITY = 32870u;
        public const uint GL_TEXTURE_RESIDENT = 32871u;
        public const uint GL_TEXTURE_BINDING_1D = 32872u;
        public const uint GL_TEXTURE_BINDING_2D = 32873u;
        public const uint GL_VERTEX_ARRAY = 32884u;
        public const uint GL_NORMAL_ARRAY = 32885u;
        public const uint GL_COLOR_ARRAY = 32886u;
        public const uint GL_INDEX_ARRAY = 32887u;
        public const uint GL_TEXTURE_COORD_ARRAY = 32888u;
        public const uint GL_EDGE_FLAG_ARRAY = 32889u;
        public const uint GL_VERTEX_ARRAY_SIZE = 32890u;
        public const uint GL_VERTEX_ARRAY_TYPE = 32891u;
        public const uint GL_VERTEX_ARRAY_STRIDE = 32892u;
        public const uint GL_NORMAL_ARRAY_TYPE = 32894u;
        public const uint GL_NORMAL_ARRAY_STRIDE = 32895u;
        public const uint GL_COLOR_ARRAY_SIZE = 32897u;
        public const uint GL_COLOR_ARRAY_TYPE = 32898u;
        public const uint GL_COLOR_ARRAY_STRIDE = 32899u;
        public const uint GL_INDEX_ARRAY_TYPE = 32901u;
        public const uint GL_INDEX_ARRAY_STRIDE = 32902u;
        public const uint GL_TEXTURE_COORD_ARRAY_SIZE = 32904u;
        public const uint GL_TEXTURE_COORD_ARRAY_TYPE = 32905u;
        public const uint GL_TEXTURE_COORD_ARRAY_STRIDE = 32906u;
        public const uint GL_EDGE_FLAG_ARRAY_STRIDE = 32908u;
        public const uint GL_VERTEX_ARRAY_POINTER = 32910u;
        public const uint GL_NORMAL_ARRAY_POINTER = 32911u;
        public const uint GL_COLOR_ARRAY_POINTER = 32912u;
        public const uint GL_INDEX_ARRAY_POINTER = 32913u;
        public const uint GL_TEXTURE_COORD_ARRAY_POINTER = 32914u;
        public const uint GL_EDGE_FLAG_ARRAY_POINTER = 32915u;
        public const uint GL_V2F = 10784u;
        public const uint GL_V3F = 10785u;
        public const uint GL_C4UB_V2F = 10786u;
        public const uint GL_C4UB_V3F = 10787u;
        public const uint GL_C3F_V3F = 10788u;
        public const uint GL_N3F_V3F = 10789u;
        public const uint GL_C4F_N3F_V3F = 10790u;
        public const uint GL_T2F_V3F = 10791u;
        public const uint GL_T4F_V4F = 10792u;
        public const uint GL_T2F_C4UB_V3F = 10793u;
        public const uint GL_T2F_C3F_V3F = 10794u;
        public const uint GL_T2F_N3F_V3F = 10795u;
        public const uint GL_T2F_C4F_N3F_V3F = 10796u;
        public const uint GL_T4F_C4F_N3F_V4F = 10797u;
        public const uint GL_EXT_vertex_array = 1u;
        public const uint GL_EXT_bgra = 1u;
        public const uint GL_EXT_paletted_texture = 1u;
        public const uint GL_WIN_swap_hint = 1u;
        public const uint GL_WIN_draw_range_elements = 1u;
        public const uint GL_VERTEX_ARRAY_EXT = 32884u;
        public const uint GL_NORMAL_ARRAY_EXT = 32885u;
        public const uint GL_COLOR_ARRAY_EXT = 32886u;
        public const uint GL_INDEX_ARRAY_EXT = 32887u;
        public const uint GL_TEXTURE_COORD_ARRAY_EXT = 32888u;
        public const uint GL_EDGE_FLAG_ARRAY_EXT = 32889u;
        public const uint GL_VERTEX_ARRAY_SIZE_EXT = 32890u;
        public const uint GL_VERTEX_ARRAY_TYPE_EXT = 32891u;
        public const uint GL_VERTEX_ARRAY_STRIDE_EXT = 32892u;
        public const uint GL_VERTEX_ARRAY_COUNT_EXT = 32893u;
        public const uint GL_NORMAL_ARRAY_TYPE_EXT = 32894u;
        public const uint GL_NORMAL_ARRAY_STRIDE_EXT = 32895u;
        public const uint GL_NORMAL_ARRAY_COUNT_EXT = 32896u;
        public const uint GL_COLOR_ARRAY_SIZE_EXT = 32897u;
        public const uint GL_COLOR_ARRAY_TYPE_EXT = 32898u;
        public const uint GL_COLOR_ARRAY_STRIDE_EXT = 32899u;
        public const uint GL_COLOR_ARRAY_COUNT_EXT = 32900u;
        public const uint GL_INDEX_ARRAY_TYPE_EXT = 32901u;
        public const uint GL_INDEX_ARRAY_STRIDE_EXT = 32902u;
        public const uint GL_INDEX_ARRAY_COUNT_EXT = 32903u;
        public const uint GL_TEXTURE_COORD_ARRAY_SIZE_EXT = 32904u;
        public const uint GL_TEXTURE_COORD_ARRAY_TYPE_EXT = 32905u;
        public const uint GL_TEXTURE_COORD_ARRAY_STRIDE_EXT = 32906u;
        public const uint GL_TEXTURE_COORD_ARRAY_COUNT_EXT = 32907u;
        public const uint GL_EDGE_FLAG_ARRAY_STRIDE_EXT = 32908u;
        public const uint GL_EDGE_FLAG_ARRAY_COUNT_EXT = 32909u;
        public const uint GL_VERTEX_ARRAY_POINTER_EXT = 32910u;
        public const uint GL_NORMAL_ARRAY_POINTER_EXT = 32911u;
        public const uint GL_COLOR_ARRAY_POINTER_EXT = 32912u;
        public const uint GL_INDEX_ARRAY_POINTER_EXT = 32913u;
        public const uint GL_TEXTURE_COORD_ARRAY_POINTER_EXT = 32914u;
        public const uint GL_EDGE_FLAG_ARRAY_POINTER_EXT = 32915u;
        public const uint GL_DOUBLE_EXT = 1u;
        public const uint GL_COLOR_TABLE_FORMAT_EXT = 32984u;
        public const uint GL_COLOR_TABLE_WIDTH_EXT = 32985u;
        public const uint GL_COLOR_TABLE_RED_SIZE_EXT = 32986u;
        public const uint GL_COLOR_TABLE_GREEN_SIZE_EXT = 32987u;
        public const uint GL_COLOR_TABLE_BLUE_SIZE_EXT = 32988u;
        public const uint GL_COLOR_TABLE_ALPHA_SIZE_EXT = 32989u;
        public const uint GL_COLOR_TABLE_LUMINANCE_SIZE_EXT = 32990u;
        public const uint GL_COLOR_TABLE_INTENSITY_SIZE_EXT = 32991u;
        public const uint GL_COLOR_INDEX1_EXT = 32994u;
        public const uint GL_COLOR_INDEX2_EXT = 32995u;
        public const uint GL_COLOR_INDEX4_EXT = 32996u;
        public const uint GL_COLOR_INDEX8_EXT = 32997u;
        public const uint GL_COLOR_INDEX12_EXT = 32998u;
        public const uint GL_COLOR_INDEX16_EXT = 32999u;
        public const uint GL_MAX_ELEMENTS_VERTICES_WIN = 33000u;
        public const uint GL_MAX_ELEMENTS_INDICES_WIN = 33001u;
        public const uint GL_PHONG_WIN = 33002u;
        public const uint GL_PHONG_HINT_WIN = 33003u;
        public const uint GL_UNSIGNED_BYTE_3_3_2 = 32818u;
        public const uint GL_UNSIGNED_SHORT_4_4_4_4 = 32819u;
        public const uint GL_UNSIGNED_SHORT_5_5_5_1 = 32820u;
        public const uint GL_UNSIGNED_INT_8_8_8_8 = 32821u;
        public const uint GL_UNSIGNED_INT_10_10_10_2 = 32822u;
        public const uint GL_TEXTURE_BINDING_3D = 32874u;
        public const uint GL_PACK_SKIP_IMAGES = 32875u;
        public const uint GL_PACK_IMAGE_HEIGHT = 32876u;
        public const uint GL_UNPACK_SKIP_IMAGES = 32877u;
        public const uint GL_UNPACK_IMAGE_HEIGHT = 32878u;
        public const uint GL_TEXTURE_3D = 32879u;
        public const uint GL_PROXY_TEXTURE_3D = 32880u;
        public const uint GL_TEXTURE_DEPTH = 32881u;
        public const uint GL_TEXTURE_WRAP_R = 32882u;
        public const uint GL_MAX_3D_TEXTURE_SIZE = 32883u;
        public const uint GL_UNSIGNED_BYTE_2_3_3_REV = 33634u;
        public const uint GL_UNSIGNED_SHORT_5_6_5 = 33635u;
        public const uint GL_UNSIGNED_SHORT_5_6_5_REV = 33636u;
        public const uint GL_UNSIGNED_SHORT_4_4_4_4_REV = 33637u;
        public const uint GL_UNSIGNED_SHORT_1_5_5_5_REV = 33638u;
        public const uint GL_UNSIGNED_INT_8_8_8_8_REV = 33639u;
        public const uint GL_UNSIGNED_INT_2_10_10_10_REV = 33640u;
        public const uint GL_BGR = 32992u;
        public const uint GL_BGRA = 32993u;
        public const uint GL_MAX_ELEMENTS_VERTICES = 33000u;
        public const uint GL_MAX_ELEMENTS_INDICES = 33001u;
        public const uint GL_CLAMP_TO_EDGE = 33071u;
        public const uint GL_TEXTURE_MIN_LOD = 33082u;
        public const uint GL_TEXTURE_MAX_LOD = 33083u;
        public const uint GL_TEXTURE_BASE_LEVEL = 33084u;
        public const uint GL_TEXTURE_MAX_LEVEL = 33085u;
        public const uint GL_SMOOTH_POINT_SIZE_RANGE = 2834u;
        public const uint GL_SMOOTH_POINT_SIZE_GRANULARITY = 2835u;
        public const uint GL_SMOOTH_LINE_WIDTH_RANGE = 2850u;
        public const uint GL_SMOOTH_LINE_WIDTH_GRANULARITY = 2851u;
        public const uint GL_ALIASED_LINE_WIDTH_RANGE = 33902u;
        public const uint GL_TEXTURE0 = 33984u;
        public const uint GL_TEXTURE1 = 33985u;
        public const uint GL_TEXTURE2 = 33986u;
        public const uint GL_TEXTURE3 = 33987u;
        public const uint GL_TEXTURE4 = 33988u;
        public const uint GL_TEXTURE5 = 33989u;
        public const uint GL_TEXTURE6 = 33990u;
        public const uint GL_TEXTURE7 = 33991u;
        public const uint GL_TEXTURE8 = 33992u;
        public const uint GL_TEXTURE9 = 33993u;
        public const uint GL_TEXTURE10 = 33994u;
        public const uint GL_TEXTURE11 = 33995u;
        public const uint GL_TEXTURE12 = 33996u;
        public const uint GL_TEXTURE13 = 33997u;
        public const uint GL_TEXTURE14 = 33998u;
        public const uint GL_TEXTURE15 = 33999u;
        public const uint GL_TEXTURE16 = 34000u;
        public const uint GL_TEXTURE17 = 34001u;
        public const uint GL_TEXTURE18 = 34002u;
        public const uint GL_TEXTURE19 = 34003u;
        public const uint GL_TEXTURE20 = 34004u;
        public const uint GL_TEXTURE21 = 34005u;
        public const uint GL_TEXTURE22 = 34006u;
        public const uint GL_TEXTURE23 = 34007u;
        public const uint GL_TEXTURE24 = 34008u;
        public const uint GL_TEXTURE25 = 34009u;
        public const uint GL_TEXTURE26 = 34010u;
        public const uint GL_TEXTURE27 = 34011u;
        public const uint GL_TEXTURE28 = 34012u;
        public const uint GL_TEXTURE29 = 34013u;
        public const uint GL_TEXTURE30 = 34014u;
        public const uint GL_TEXTURE31 = 34015u;
        public const uint GL_ACTIVE_TEXTURE = 34016u;
        public const uint GL_MULTISAMPLE = 32925u;
        public const uint GL_SAMPLE_ALPHA_TO_COVERAGE = 32926u;
        public const uint GL_SAMPLE_ALPHA_TO_ONE = 32927u;
        public const uint GL_SAMPLE_COVERAGE = 32928u;
        public const uint GL_SAMPLE_BUFFERS = 32936u;
        public const uint GL_SAMPLES = 32937u;
        public const uint GL_SAMPLE_COVERAGE_VALUE = 32938u;
        public const uint GL_SAMPLE_COVERAGE_INVERT = 32939u;
        public const uint GL_TEXTURE_CUBE_MAP = 34067u;
        public const uint GL_TEXTURE_BINDING_CUBE_MAP = 34068u;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_X = 34069u;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_X = 34070u;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Y = 34071u;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Y = 34072u;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Z = 34073u;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Z = 34074u;
        public const uint GL_PROXY_TEXTURE_CUBE_MAP = 34075u;
        public const uint GL_MAX_CUBE_MAP_TEXTURE_SIZE = 34076u;
        public const uint GL_COMPRESSED_RGB = 34029u;
        public const uint GL_COMPRESSED_RGBA = 34030u;
        public const uint GL_TEXTURE_COMPRESSION_HINT = 34031u;
        public const uint GL_TEXTURE_COMPRESSED_IMAGE_SIZE = 34464u;
        public const uint GL_TEXTURE_COMPRESSED = 34465u;
        public const uint GL_NUM_COMPRESSED_TEXTURE_FORMATS = 34466u;
        public const uint GL_COMPRESSED_TEXTURE_FORMATS = 34467u;
        public const uint GL_CLAMP_TO_BORDER = 33069u;
        public const uint GL_BLEND_DST_RGB = 32968u;
        public const uint GL_BLEND_SRC_RGB = 32969u;
        public const uint GL_BLEND_DST_ALPHA = 32970u;
        public const uint GL_BLEND_SRC_ALPHA = 32971u;
        public const uint GL_POINT_FADE_THRESHOLD_SIZE = 33064u;
        public const uint GL_DEPTH_COMPONENT16 = 33189u;
        public const uint GL_DEPTH_COMPONENT24 = 33190u;
        public const uint GL_DEPTH_COMPONENT32 = 33191u;
        public const uint GL_MIRRORED_REPEAT = 33648u;
        public const uint GL_MAX_TEXTURE_LOD_BIAS = 34045u;
        public const uint GL_TEXTURE_LOD_BIAS = 34049u;
        public const uint GL_INCR_WRAP = 34055u;
        public const uint GL_DECR_WRAP = 34056u;
        public const uint GL_TEXTURE_DEPTH_SIZE = 34890u;
        public const uint GL_TEXTURE_COMPARE_MODE = 34892u;
        public const uint GL_TEXTURE_COMPARE_FUNC = 34893u;
        public const uint GL_BUFFER_SIZE = 34660u;
        public const uint GL_BUFFER_USAGE = 34661u;
        public const uint GL_QUERY_COUNTER_BITS = 34916u;
        public const uint GL_CURRENT_QUERY = 34917u;
        public const uint GL_QUERY_RESULT = 34918u;
        public const uint GL_QUERY_RESULT_AVAILABLE = 34919u;
        public const uint GL_ARRAY_BUFFER = 34962u;
        public const uint GL_ELEMENT_ARRAY_BUFFER = 34963u;
        public const uint GL_ARRAY_BUFFER_BINDING = 34964u;
        public const uint GL_ELEMENT_ARRAY_BUFFER_BINDING = 34965u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 34975u;
        public const uint GL_READ_ONLY = 35000u;
        public const uint GL_WRITE_ONLY = 35001u;
        public const uint GL_READ_WRITE = 35002u;
        public const uint GL_BUFFER_ACCESS = 35003u;
        public const uint GL_BUFFER_MAPPED = 35004u;
        public const uint GL_BUFFER_MAP_POINTER = 35005u;
        public const uint GL_STREAM_DRAW = 35040u;
        public const uint GL_STREAM_READ = 35041u;
        public const uint GL_STREAM_COPY = 35042u;
        public const uint GL_STATIC_DRAW = 35044u;
        public const uint GL_STATIC_READ = 35045u;
        public const uint GL_STATIC_COPY = 35046u;
        public const uint GL_DYNAMIC_DRAW = 35048u;
        public const uint GL_DYNAMIC_READ = 35049u;
        public const uint GL_DYNAMIC_COPY = 35050u;
        public const uint GL_SAMPLES_PASSED = 35092u;
        public const uint GL_BLEND_EQUATION_RGB = 32777u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_ENABLED = 34338u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_SIZE = 34339u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_STRIDE = 34340u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_TYPE = 34341u;
        public const uint GL_CURRENT_VERTEX_ATTRIB = 34342u;
        public const uint GL_VERTEX_PROGRAM_POINT_SIZE = 34370u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_POINTER = 34373u;
        public const uint GL_STENCIL_BACK_FUNC = 34816u;
        public const uint GL_STENCIL_BACK_FAIL = 34817u;
        public const uint GL_STENCIL_BACK_PASS_DEPTH_FAIL = 34818u;
        public const uint GL_STENCIL_BACK_PASS_DEPTH_PASS = 34819u;
        public const uint GL_MAX_DRAW_BUFFERS = 34852u;
        public const uint GL_DRAW_BUFFER0 = 34853u;
        public const uint GL_DRAW_BUFFER1 = 34854u;
        public const uint GL_DRAW_BUFFER2 = 34855u;
        public const uint GL_DRAW_BUFFER3 = 34856u;
        public const uint GL_DRAW_BUFFER4 = 34857u;
        public const uint GL_DRAW_BUFFER5 = 34858u;
        public const uint GL_DRAW_BUFFER6 = 34859u;
        public const uint GL_DRAW_BUFFER7 = 34860u;
        public const uint GL_DRAW_BUFFER8 = 34861u;
        public const uint GL_DRAW_BUFFER9 = 34862u;
        public const uint GL_DRAW_BUFFER10 = 34863u;
        public const uint GL_DRAW_BUFFER11 = 34864u;
        public const uint GL_DRAW_BUFFER12 = 34865u;
        public const uint GL_DRAW_BUFFER13 = 34866u;
        public const uint GL_DRAW_BUFFER14 = 34867u;
        public const uint GL_DRAW_BUFFER15 = 34868u;
        public const uint GL_BLEND_EQUATION_ALPHA = 34877u;
        public const uint GL_MAX_VERTEX_ATTRIBS = 34921u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_NORMALIZED = 34922u;
        public const uint GL_MAX_TEXTURE_IMAGE_UNITS = 34930u;
        public const uint GL_FRAGMENT_SHADER = 35632u;
        public const uint GL_VERTEX_SHADER = 35633u;
        public const uint GL_MAX_FRAGMENT_UNIFORM_COMPONENTS = 35657u;
        public const uint GL_MAX_VERTEX_UNIFORM_COMPONENTS = 35658u;
        public const uint GL_MAX_VARYING_FLOATS = 35659u;
        public const uint GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS = 35660u;
        public const uint GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS = 35661u;
        public const uint GL_SHADER_TYPE = 35663u;
        public const uint GL_FLOAT_VEC2 = 35664u;
        public const uint GL_FLOAT_VEC3 = 35665u;
        public const uint GL_FLOAT_VEC4 = 35666u;
        public const uint GL_INT_VEC2 = 35667u;
        public const uint GL_INT_VEC3 = 35668u;
        public const uint GL_INT_VEC4 = 35669u;
        public const uint GL_BOOL = 35670u;
        public const uint GL_BOOL_VEC2 = 35671u;
        public const uint GL_BOOL_VEC3 = 35672u;
        public const uint GL_BOOL_VEC4 = 35673u;
        public const uint GL_FLOAT_MAT2 = 35674u;
        public const uint GL_FLOAT_MAT3 = 35675u;
        public const uint GL_FLOAT_MAT4 = 35676u;
        public const uint GL_SAMPLER_1D = 35677u;
        public const uint GL_SAMPLER_2D = 35678u;
        public const uint GL_SAMPLER_3D = 35679u;
        public const uint GL_SAMPLER_CUBE = 35680u;
        public const uint GL_SAMPLER_1D_SHADOW = 35681u;
        public const uint GL_SAMPLER_2D_SHADOW = 35682u;
        public const uint GL_DELETE_STATUS = 35712u;
        public const uint GL_COMPILE_STATUS = 35713u;
        public const uint GL_LINK_STATUS = 35714u;
        public const uint GL_VALIDATE_STATUS = 35715u;
        public const uint GL_INFO_LOG_LENGTH = 35716u;
        public const uint GL_ATTACHED_SHADERS = 35717u;
        public const uint GL_ACTIVE_UNIFORMS = 35718u;
        public const uint GL_ACTIVE_UNIFORM_MAX_LENGTH = 35719u;
        public const uint GL_SHADER_SOURCE_LENGTH = 35720u;
        public const uint GL_ACTIVE_ATTRIBUTES = 35721u;
        public const uint GL_ACTIVE_ATTRIBUTE_MAX_LENGTH = 35722u;
        public const uint GL_FRAGMENT_SHADER_DERIVATIVE_HINT = 35723u;
        public const uint GL_SHADING_LANGUAGE_VERSION = 35724u;
        public const uint GL_CURRENT_PROGRAM = 35725u;
        public const uint GL_POINT_SPRITE_COORD_ORIGIN = 36000u;
        public const uint GL_LOWER_LEFT = 36001u;
        public const uint GL_UPPER_LEFT = 36002u;
        public const uint GL_STENCIL_BACK_REF = 36003u;
        public const uint GL_STENCIL_BACK_VALUE_MASK = 36004u;
        public const uint GL_STENCIL_BACK_WRITEMASK = 36005u;
        public const uint GL_PIXEL_PACK_BUFFER = 35051u;
        public const uint GL_PIXEL_UNPACK_BUFFER = 35052u;
        public const uint GL_PIXEL_PACK_BUFFER_BINDING = 35053u;
        public const uint GL_PIXEL_UNPACK_BUFFER_BINDING = 35055u;
        public const uint GL_FLOAT_MAT2x3 = 35685u;
        public const uint GL_FLOAT_MAT2x4 = 35686u;
        public const uint GL_FLOAT_MAT3x2 = 35687u;
        public const uint GL_FLOAT_MAT3x4 = 35688u;
        public const uint GL_FLOAT_MAT4x2 = 35689u;
        public const uint GL_FLOAT_MAT4x3 = 35690u;
        public const uint GL_SRGB = 35904u;
        public const uint GL_SRGB8 = 35905u;
        public const uint GL_SRGB_ALPHA = 35906u;
        public const uint GL_SRGB8_ALPHA8 = 35907u;
        public const uint GL_COMPRESSED_SRGB = 35912u;
        public const uint GL_COMPRESSED_SRGB_ALPHA = 35913u;
        public const uint GL_COMPARE_REF_TO_TEXTURE = 34894u;
        public const uint GL_CLIP_DISTANCE0 = 12288u;
        public const uint GL_CLIP_DISTANCE1 = 12289u;
        public const uint GL_CLIP_DISTANCE2 = 12290u;
        public const uint GL_CLIP_DISTANCE3 = 12291u;
        public const uint GL_CLIP_DISTANCE4 = 12292u;
        public const uint GL_CLIP_DISTANCE5 = 12293u;
        public const uint GL_CLIP_DISTANCE6 = 12294u;
        public const uint GL_CLIP_DISTANCE7 = 12295u;
        public const uint GL_MAX_CLIP_DISTANCES = 3378u;
        public const uint GL_MAJOR_VERSION = 33307u;
        public const uint GL_MINOR_VERSION = 33308u;
        public const uint GL_NUM_EXTENSIONS = 33309u;
        public const uint GL_CONTEXT_FLAGS = 33310u;
        public const uint GL_DEPTH_BUFFER = 33315u;
        public const uint GL_STENCIL_BUFFER = 33316u;
        public const uint GL_COMPRESSED_RED = 33317u;
        public const uint GL_COMPRESSED_RG = 33318u;
        public const uint GL_CONTEXT_FLAG_FORWARD_COMPATIBLE_BIT = 1u;
        public const uint GL_RGBA32F = 34836u;
        public const uint GL_RGB32F = 34837u;
        public const uint GL_RGBA16F = 34842u;
        public const uint GL_RGB16F = 34843u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_INTEGER = 35069u;
        public const uint GL_MAX_ARRAY_TEXTURE_LAYERS = 35071u;
        public const uint GL_MIN_PROGRAM_TEXEL_OFFSET = 35076u;
        public const uint GL_MAX_PROGRAM_TEXEL_OFFSET = 35077u;
        public const uint GL_CLAMP_READ_COLOR = 35100u;
        public const uint GL_FIXED_ONLY = 35101u;
        public const uint GL_MAX_VARYING_COMPONENTS = 35659u;
        public const uint GL_TEXTURE_1D_ARRAY = 35864u;
        public const uint GL_PROXY_TEXTURE_1D_ARRAY = 35865u;
        public const uint GL_TEXTURE_2D_ARRAY = 35866u;
        public const uint GL_PROXY_TEXTURE_2D_ARRAY = 35867u;
        public const uint GL_TEXTURE_BINDING_1D_ARRAY = 35868u;
        public const uint GL_TEXTURE_BINDING_2D_ARRAY = 35869u;
        public const uint GL_R11F_G11F_B10F = 35898u;
        public const uint GL_UNSIGNED_INT_10F_11F_11F_REV = 35899u;
        public const uint GL_RGB9_E5 = 35901u;
        public const uint GL_UNSIGNED_INT_5_9_9_9_REV = 35902u;
        public const uint GL_TEXTURE_SHARED_SIZE = 35903u;
        public const uint GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH = 35958u;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_MODE = 35967u;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS = 35968u;
        public const uint GL_TRANSFORM_FEEDBACK_VARYINGS = 35971u;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_START = 35972u;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_SIZE = 35973u;
        public const uint GL_PRIMITIVES_GENERATED = 35975u;
        public const uint GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN = 35976u;
        public const uint GL_RASTERIZER_DISCARD = 35977u;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS = 35978u;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS = 35979u;
        public const uint GL_INTERLEAVED_ATTRIBS = 35980u;
        public const uint GL_SEPARATE_ATTRIBS = 35981u;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER = 35982u;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_BINDING = 35983u;
        public const uint GL_RGBA32UI = 36208u;
        public const uint GL_RGB32UI = 36209u;
        public const uint GL_RGBA16UI = 36214u;
        public const uint GL_RGB16UI = 36215u;
        public const uint GL_RGBA8UI = 36220u;
        public const uint GL_RGB8UI = 36221u;
        public const uint GL_RGBA32I = 36226u;
        public const uint GL_RGB32I = 36227u;
        public const uint GL_RGBA16I = 36232u;
        public const uint GL_RGB16I = 36233u;
        public const uint GL_RGBA8I = 36238u;
        public const uint GL_RGB8I = 36239u;
        public const uint GL_RED_INTEGER = 36244u;
        public const uint GL_GREEN_INTEGER = 36245u;
        public const uint GL_BLUE_INTEGER = 36246u;
        public const uint GL_RGB_INTEGER = 36248u;
        public const uint GL_RGBA_INTEGER = 36249u;
        public const uint GL_BGR_INTEGER = 36250u;
        public const uint GL_BGRA_INTEGER = 36251u;
        public const uint GL_SAMPLER_1D_ARRAY = 36288u;
        public const uint GL_SAMPLER_2D_ARRAY = 36289u;
        public const uint GL_SAMPLER_1D_ARRAY_SHADOW = 36291u;
        public const uint GL_SAMPLER_2D_ARRAY_SHADOW = 36292u;
        public const uint GL_SAMPLER_CUBE_SHADOW = 36293u;
        public const uint GL_UNSIGNED_INT_VEC2 = 36294u;
        public const uint GL_UNSIGNED_INT_VEC3 = 36295u;
        public const uint GL_UNSIGNED_INT_VEC4 = 36296u;
        public const uint GL_INT_SAMPLER_1D = 36297u;
        public const uint GL_INT_SAMPLER_2D = 36298u;
        public const uint GL_INT_SAMPLER_3D = 36299u;
        public const uint GL_INT_SAMPLER_CUBE = 36300u;
        public const uint GL_INT_SAMPLER_1D_ARRAY = 36302u;
        public const uint GL_INT_SAMPLER_2D_ARRAY = 36303u;
        public const uint GL_UNSIGNED_INT_SAMPLER_1D = 36305u;
        public const uint GL_UNSIGNED_INT_SAMPLER_2D = 36306u;
        public const uint GL_UNSIGNED_INT_SAMPLER_3D = 36307u;
        public const uint GL_UNSIGNED_INT_SAMPLER_CUBE = 36308u;
        public const uint GL_UNSIGNED_INT_SAMPLER_1D_ARRAY = 36310u;
        public const uint GL_UNSIGNED_INT_SAMPLER_2D_ARRAY = 36311u;
        public const uint GL_QUERY_WAIT = 36371u;
        public const uint GL_QUERY_NO_WAIT = 36372u;
        public const uint GL_QUERY_BY_REGION_WAIT = 36373u;
        public const uint GL_QUERY_BY_REGION_NO_WAIT = 36374u;
        public const uint GL_BUFFER_ACCESS_FLAGS = 37151u;
        public const uint GL_BUFFER_MAP_LENGTH = 37152u;
        public const uint GL_BUFFER_MAP_OFFSET = 37153u;
        public const uint GL_SAMPLER_2D_RECT = 35683u;
        public const uint GL_SAMPLER_2D_RECT_SHADOW = 35684u;
        public const uint GL_SAMPLER_BUFFER = 36290u;
        public const uint GL_INT_SAMPLER_2D_RECT = 36301u;
        public const uint GL_INT_SAMPLER_BUFFER = 36304u;
        public const uint GL_UNSIGNED_INT_SAMPLER_2D_RECT = 36309u;
        public const uint GL_UNSIGNED_INT_SAMPLER_BUFFER = 36312u;
        public const uint GL_TEXTURE_BUFFER = 35882u;
        public const uint GL_MAX_TEXTURE_BUFFER_SIZE = 35883u;
        public const uint GL_TEXTURE_BINDING_BUFFER = 35884u;
        public const uint GL_TEXTURE_BUFFER_DATA_STORE_BINDING = 35885u;
        public const uint GL_TEXTURE_BUFFER_FORMAT = 35886u;
        public const uint GL_TEXTURE_RECTANGLE = 34037u;
        public const uint GL_TEXTURE_BINDING_RECTANGLE = 34038u;
        public const uint GL_PROXY_TEXTURE_RECTANGLE = 34039u;
        public const uint GL_MAX_RECTANGLE_TEXTURE_SIZE = 34040u;
        public const uint GL_RED_SNORM = 36752u;
        public const uint GL_RG_SNORM = 36753u;
        public const uint GL_RGB_SNORM = 36754u;
        public const uint GL_RGBA_SNORM = 36755u;
        public const uint GL_R8_SNORM = 36756u;
        public const uint GL_RG8_SNORM = 36757u;
        public const uint GL_RGB8_SNORM = 36758u;
        public const uint GL_RGBA8_SNORM = 36759u;
        public const uint GL_R16_SNORM = 36760u;
        public const uint GL_RG16_SNORM = 36761u;
        public const uint GL_RGB16_SNORM = 36762u;
        public const uint GL_RGBA16_SNORM = 36763u;
        public const uint GL_SIGNED_NORMALIZED = 36764u;
        public const uint GL_PRIMITIVE_RESTART = 36765u;
        public const uint GL_PRIMITIVE_RESTART_INDEX = 36766u;
        public const uint GL_CONTEXT_CORE_PROFILE_BIT = 1u;
        public const uint GL_CONTEXT_COMPATIBILITY_PROFILE_BIT = 2u;
        public const uint GL_LINES_ADJACENCY = 10u;
        public const uint GL_LINE_STRIP_ADJACENCY = 11u;
        public const uint GL_TRIANGLES_ADJACENCY = 12u;
        public const uint GL_TRIANGLE_STRIP_ADJACENCY = 13u;
        public const uint GL_PROGRAM_POINT_SIZE = 34370u;
        public const uint GL_MAX_GEOMETRY_TEXTURE_IMAGE_UNITS = 35881u;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_LAYERED = 36263u;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS = 36264u;
        public const uint GL_GEOMETRY_SHADER = 36313u;
        public const uint GL_GEOMETRY_VERTICES_OUT = 35094u;
        public const uint GL_GEOMETRY_INPUT_TYPE = 35095u;
        public const uint GL_GEOMETRY_OUTPUT_TYPE = 35096u;
        public const uint GL_MAX_GEOMETRY_UNIFORM_COMPONENTS = 36319u;
        public const uint GL_MAX_GEOMETRY_OUTPUT_VERTICES = 36320u;
        public const uint GL_MAX_GEOMETRY_TOTAL_OUTPUT_COMPONENTS = 36321u;
        public const uint GL_MAX_VERTEX_OUTPUT_COMPONENTS = 37154u;
        public const uint GL_MAX_GEOMETRY_INPUT_COMPONENTS = 37155u;
        public const uint GL_MAX_GEOMETRY_OUTPUT_COMPONENTS = 37156u;
        public const uint GL_MAX_FRAGMENT_INPUT_COMPONENTS = 37157u;
        public const uint GL_CONTEXT_PROFILE_MASK = 37158u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_DIVISOR = 35070u;
        public const uint GL_SAMPLE_SHADING = 35894u;
        public const uint GL_MIN_SAMPLE_SHADING_VALUE = 35895u;
        public const uint GL_MIN_PROGRAM_TEXTURE_GATHER_OFFSET = 36446u;
        public const uint GL_MAX_PROGRAM_TEXTURE_GATHER_OFFSET = 36447u;
        public const uint GL_TEXTURE_CUBE_MAP_ARRAY = 36873u;
        public const uint GL_TEXTURE_BINDING_CUBE_MAP_ARRAY = 36874u;
        public const uint GL_PROXY_TEXTURE_CUBE_MAP_ARRAY = 36875u;
        public const uint GL_SAMPLER_CUBE_MAP_ARRAY = 36876u;
        public const uint GL_SAMPLER_CUBE_MAP_ARRAY_SHADOW = 36877u;
        public const uint GL_INT_SAMPLER_CUBE_MAP_ARRAY = 36878u;
        public const uint GL_UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY = 36879u;
        public const uint GL_BGR_EXT = 32992u;
        public const uint GL_BGRA_EXT = 32993u;
        public const uint GL_UNSIGNED_BYTE_3_3_2_EXT = 32818u;
        public const uint GL_UNSIGNED_SHORT_4_4_4_4_EXT = 32819u;
        public const uint GL_UNSIGNED_SHORT_5_5_5_1_EXT = 32820u;
        public const uint GL_UNSIGNED_INT_8_8_8_8_EXT = 32821u;
        public const uint GL_UNSIGNED_INT_10_10_10_2_EXT = 32822u;
        public const uint GL_RESCALE_NORMAL_EXT = 32826u;
        public const uint GL_LIGHT_MODEL_COLOR_CONTROL_EXT = 33272u;
        public const uint GL_SINGLE_COLOR_EXT = 33273u;
        public const uint GL_SEPARATE_SPECULAR_COLOR_EXT = 33274u;
        public const uint GL_CLAMP_TO_EDGE_SGIS = 33071u;
        public const uint GL_TEXTURE_MIN_LOD_SGIS = 33082u;
        public const uint GL_TEXTURE_MAX_LOD_SGIS = 33083u;
        public const uint GL_TEXTURE_BASE_LEVEL_SGIS = 33084u;
        public const uint GL_TEXTURE_MAX_LEVEL_SGIS = 33085u;
        public const uint GL_MAX_ELEMENTS_VERTICES_EXT = 33000u;
        public const uint GL_MAX_ELEMENTS_INDICES_EXT = 33001u;
        public const uint GL_COLOR_TABLE_SGI = 32976u;
        public const uint GL_POST_CONVOLUTION_COLOR_TABLE_SGI = 32977u;
        public const uint GL_POST_COLOR_MATRIX_COLOR_TABLE_SGI = 32978u;
        public const uint GL_PROXY_COLOR_TABLE_SGI = 32979u;
        public const uint GL_PROXY_POST_CONVOLUTION_COLOR_TABLE_SGI = 32980u;
        public const uint GL_PROXY_POST_COLOR_MATRIX_COLOR_TABLE_SGI = 32981u;
        public const uint GL_COLOR_TABLE_SCALE_SGI = 32982u;
        public const uint GL_COLOR_TABLE_BIAS_SGI = 32983u;
        public const uint GL_COLOR_TABLE_FORMAT_SGI = 32984u;
        public const uint GL_COLOR_TABLE_WIDTH_SGI = 32985u;
        public const uint GL_COLOR_TABLE_RED_SIZE_SGI = 32986u;
        public const uint GL_COLOR_TABLE_GREEN_SIZE_SGI = 32987u;
        public const uint GL_COLOR_TABLE_BLUE_SIZE_SGI = 32988u;
        public const uint GL_COLOR_TABLE_ALPHA_SIZE_SGI = 32989u;
        public const uint GL_COLOR_TABLE_LUMINANCE_SIZE_SGI = 32990u;
        public const uint GL_COLOR_TABLE_INTENSITY_SIZE_SGI = 32991u;
        public const uint GL_COLOR_MATRIX_SGI = 32945u;
        public const uint GL_COLOR_MATRIX_STACK_DEPTH_SGI = 32946u;
        public const uint GL_MAX_COLOR_MATRIX_STACK_DEPTH_SGI = 32947u;
        public const uint GL_POST_COLOR_MATRIX_RED_SCALE_SGI = 32948u;
        public const uint GL_POST_COLOR_MATRIX_GREEN_SCALE_SGI = 32949u;
        public const uint GL_POST_COLOR_MATRIX_BLUE_SCALE_SGI = 32950u;
        public const uint GL_POST_COLOR_MATRIX_ALPHA_SCALE_SGI = 32951u;
        public const uint GL_POST_COLOR_MATRIX_RED_BIAS_SGI = 32952u;
        public const uint GL_POST_COLOR_MATRIX_GREEN_BIAS_SGI = 32953u;
        public const uint GL_POST_COLOR_MATRIX_BLUE_BIAS_SGI = 32954u;
        public const uint GL_POST_COLOR_MATRIX_ALPHA_BIAS_SGI = 32955u;
        public const uint GL_HISTOGRAM_EXT = 32804u;
        public const uint GL_PROXY_HISTOGRAM_EXT = 32805u;
        public const uint GL_HISTOGRAM_WIDTH_EXT = 32806u;
        public const uint GL_HISTOGRAM_FORMAT_EXT = 32807u;
        public const uint GL_HISTOGRAM_RED_SIZE_EXT = 32808u;
        public const uint GL_HISTOGRAM_GREEN_SIZE_EXT = 32809u;
        public const uint GL_HISTOGRAM_BLUE_SIZE_EXT = 32810u;
        public const uint GL_HISTOGRAM_ALPHA_SIZE_EXT = 32811u;
        public const uint GL_HISTOGRAM_LUMINANCE_SIZE_EXT = 32812u;
        public const uint GL_HISTOGRAM_SINK_EXT = 32813u;
        public const uint GL_MINMAX_EXT = 32814u;
        public const uint GL_MINMAX_FORMAT_EXT = 32815u;
        public const uint GL_MINMAX_SINK_EXT = 32816u;
        public const uint GL_TABLE_TOO_LARGE_EXT = 32817u;
        public const uint GL_CONSTANT_COLOR_EXT = 32769u;
        public const uint GL_ONE_MINUS_CONSTANT_COLOR_EXT = 32770u;
        public const uint GL_CONSTANT_ALPHA_EXT = 32771u;
        public const uint GL_ONE_MINUS_CONSTANT_ALPHA_EXT = 32772u;
        public const uint GL_BLEND_COLOR_EXT = 32773u;
        public const uint GL_FUNC_ADD_EXT = 32774u;
        public const uint GL_MIN_EXT = 32775u;
        public const uint GL_MAX_EXT = 32776u;
        public const uint GL_FUNC_SUBTRACT_EXT = 32778u;
        public const uint GL_FUNC_REVERSE_SUBTRACT_EXT = 32779u;
        public const uint GL_BLEND_EQUATION_EXT = 32777u;
        public const uint GL_TEXTURE0_ARB = 33984u;
        public const uint GL_TEXTURE1_ARB = 33985u;
        public const uint GL_TEXTURE2_ARB = 33986u;
        public const uint GL_TEXTURE3_ARB = 33987u;
        public const uint GL_TEXTURE4_ARB = 33988u;
        public const uint GL_TEXTURE5_ARB = 33989u;
        public const uint GL_TEXTURE6_ARB = 33990u;
        public const uint GL_TEXTURE7_ARB = 33991u;
        public const uint GL_TEXTURE8_ARB = 33992u;
        public const uint GL_TEXTURE9_ARB = 33993u;
        public const uint GL_TEXTURE10_ARB = 33994u;
        public const uint GL_TEXTURE11_ARB = 33995u;
        public const uint GL_TEXTURE12_ARB = 33996u;
        public const uint GL_TEXTURE13_ARB = 33997u;
        public const uint GL_TEXTURE14_ARB = 33998u;
        public const uint GL_TEXTURE15_ARB = 33999u;
        public const uint GL_TEXTURE16_ARB = 34000u;
        public const uint GL_TEXTURE17_ARB = 34001u;
        public const uint GL_TEXTURE18_ARB = 34002u;
        public const uint GL_TEXTURE19_ARB = 34003u;
        public const uint GL_TEXTURE20_ARB = 34004u;
        public const uint GL_TEXTURE21_ARB = 34005u;
        public const uint GL_TEXTURE22_ARB = 34006u;
        public const uint GL_TEXTURE23_ARB = 34007u;
        public const uint GL_TEXTURE24_ARB = 34008u;
        public const uint GL_TEXTURE25_ARB = 34009u;
        public const uint GL_TEXTURE26_ARB = 34010u;
        public const uint GL_TEXTURE27_ARB = 34011u;
        public const uint GL_TEXTURE28_ARB = 34012u;
        public const uint GL_TEXTURE29_ARB = 34013u;
        public const uint GL_TEXTURE30_ARB = 34014u;
        public const uint GL_TEXTURE31_ARB = 34015u;
        public const uint GL_ACTIVE_TEXTURE_ARB = 34016u;
        public const uint GL_CLIENT_ACTIVE_TEXTURE_ARB = 34017u;
        public const uint GL_MAX_TEXTURE_UNITS_ARB = 34018u;
        public const uint GL_COMPRESSED_ALPHA_ARB = 34025u;
        public const uint GL_COMPRESSED_LUMINANCE_ARB = 34026u;
        public const uint GL_COMPRESSED_LUMINANCE_ALPHA_ARB = 34027u;
        public const uint GL_COMPRESSED_INTENSITY_ARB = 34028u;
        public const uint GL_COMPRESSED_RGB_ARB = 34029u;
        public const uint GL_COMPRESSED_RGBA_ARB = 34030u;
        public const uint GL_TEXTURE_COMPRESSION_HINT_ARB = 34031u;
        public const uint GL_TEXTURE_COMPRESSED_IMAGE_SIZE_ARB = 34464u;
        public const uint GL_TEXTURE_COMPRESSED_ARB = 34465u;
        public const uint GL_NUM_COMPRESSED_TEXTURE_FORMATS_ARB = 34466u;
        public const uint GL_COMPRESSED_TEXTURE_FORMATS_ARB = 34467u;
        public const uint GL_NORMAL_MAP_EXT = 34065u;
        public const uint GL_REFLECTION_MAP_EXT = 34066u;
        public const uint GL_TEXTURE_CUBE_MAP_EXT = 34067u;
        public const uint GL_TEXTURE_BINDING_CUBE_MAP_EXT = 34068u;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_X_EXT = 34069u;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_X_EXT = 34070u;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Y_EXT = 34071u;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Y_EXT = 34072u;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Z_EXT = 34073u;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Z_EXT = 34074u;
        public const uint GL_PROXY_TEXTURE_CUBE_MAP_EXT = 34075u;
        public const uint GL_MAX_CUBE_MAP_TEXTURE_SIZE_EXT = 34076u;
        public const uint GL_MULTISAMPLE_ARB = 32925u;
        public const uint GL_SAMPLE_ALPHA_TO_COVERAGE_ARB = 32926u;
        public const uint GL_SAMPLE_ALPHA_TO_ONE_ARB = 32927u;
        public const uint GL_SAMPLE_COVERAGE_ARB = 32928u;
        public const uint GL_SAMPLE_BUFFERS_ARB = 32936u;
        public const uint GL_SAMPLES_ARB = 32937u;
        public const uint GL_SAMPLE_COVERAGE_VALUE_ARB = 32938u;
        public const uint GL_SAMPLE_COVERAGE_INVERT_ARB = 32939u;
        public const uint GL_MULTISAMPLE_BIT_ARB = 536870912u;
        public const uint GL_COMBINE_ARB = 34160u;
        public const uint GL_COMBINE_RGB_ARB = 34161u;
        public const uint GL_COMBINE_ALPHA_ARB = 34162u;
        public const uint GL_SOURCE0_RGB_ARB = 34176u;
        public const uint GL_SOURCE1_RGB_ARB = 34177u;
        public const uint GL_SOURCE2_RGB_ARB = 34178u;
        public const uint GL_SOURCE0_ALPHA_ARB = 34184u;
        public const uint GL_SOURCE1_ALPHA_ARB = 34185u;
        public const uint GL_SOURCE2_ALPHA_ARB = 34186u;
        public const uint GL_OPERAND0_RGB_ARB = 34192u;
        public const uint GL_OPERAND1_RGB_ARB = 34193u;
        public const uint GL_OPERAND2_RGB_ARB = 34194u;
        public const uint GL_OPERAND0_ALPHA_ARB = 34200u;
        public const uint GL_OPERAND1_ALPHA_ARB = 34201u;
        public const uint GL_OPERAND2_ALPHA_ARB = 34202u;
        public const uint GL_RGB_SCALE_ARB = 34163u;
        public const uint GL_ADD_SIGNED_ARB = 34164u;
        public const uint GL_INTERPOLATE_ARB = 34165u;
        public const uint GL_SUBTRACT_ARB = 34023u;
        public const uint GL_CONSTANT_ARB = 34166u;
        public const uint GL_PRIMARY_COLOR_ARB = 34167u;
        public const uint GL_PREVIOUS_ARB = 34168u;
        public const uint GL_DOT3_RGB_ARB = 34478u;
        public const uint GL_DOT3_RGBA_ARB = 34479u;
        public const uint GL_CLAMP_TO_BORDER_ARB = 33069u;
        public const uint GL_TRANSPOSE_MODELVIEW_MATRIX_ARB = 34019u;
        public const uint GL_TRANSPOSE_PROJECTION_MATRIX_ARB = 34020u;
        public const uint GL_TRANSPOSE_TEXTURE_MATRIX_ARB = 34021u;
        public const uint GL_TRANSPOSE_COLOR_MATRIX_ARB = 34022u;
        public const uint GL_GENERATE_MIPMAP_SGIS = 33169u;
        public const uint GL_GENERATE_MIPMAP_HINT_SGIS = 33170u;
        public const uint GL_DEPTH_COMPONENT16_ARB = 33189u;
        public const uint GL_DEPTH_COMPONENT24_ARB = 33190u;
        public const uint GL_DEPTH_COMPONENT32_ARB = 33191u;
        public const uint GL_TEXTURE_DEPTH_SIZE_ARB = 34890u;
        public const uint GL_DEPTH_TEXTURE_MODE_ARB = 34891u;
        public const uint GL_TEXTURE_COMPARE_MODE_ARB = 34892u;
        public const uint GL_TEXTURE_COMPARE_FUNC_ARB = 34893u;
        public const uint GL_COMPARE_R_TO_TEXTURE_ARB = 34894u;
        public const uint GL_FOG_COORDINATE_SOURCE_EXT = 33872u;
        public const uint GL_FOG_COORDINATE_EXT = 33873u;
        public const uint GL_FRAGMENT_DEPTH_EXT = 33874u;
        public const uint GL_CURRENT_FOG_COORDINATE_EXT = 33875u;
        public const uint GL_FOG_COORDINATE_ARRAY_TYPE_EXT = 33876u;
        public const uint GL_FOG_COORDINATE_ARRAY_STRIDE_EXT = 33877u;
        public const uint GL_FOG_COORDINATE_ARRAY_POINTER_EXT = 33878u;
        public const uint GL_FOG_COORDINATE_ARRAY_EXT = 33879u;
        public const uint GL_POINT_SIZE_MIN_ARB = 33062u;
        public const uint GL_POINT_SIZE_MAX_ARB = 33063u;
        public const uint GL_POINT_FADE_THRESHOLD_SIZE_ARB = 33064u;
        public const uint GL_POINT_DISTANCE_ATTENUATION_ARB = 33065u;
        public const uint GL_COLOR_SUM_EXT = 33880u;
        public const uint GL_CURRENT_SECONDARY_COLOR_EXT = 33881u;
        public const uint GL_SECONDARY_COLOR_ARRAY_SIZE_EXT = 33882u;
        public const uint GL_SECONDARY_COLOR_ARRAY_TYPE_EXT = 33883u;
        public const uint GL_SECONDARY_COLOR_ARRAY_STRIDE_EXT = 33884u;
        public const uint GL_SECONDARY_COLOR_ARRAY_POINTER_EXT = 33885u;
        public const uint GL_SECONDARY_COLOR_ARRAY_EXT = 33886u;
        public const uint GL_BLEND_DST_RGB_EXT = 32968u;
        public const uint GL_BLEND_SRC_RGB_EXT = 32969u;
        public const uint GL_BLEND_DST_ALPHA_EXT = 32970u;
        public const uint GL_BLEND_SRC_ALPHA_EXT = 32971u;
        public const uint GL_INCR_WRAP_EXT = 34055u;
        public const uint GL_DECR_WRAP_EXT = 34056u;
        public const uint GL_MAX_TEXTURE_LOD_BIAS_EXT = 34045u;
        public const uint GL_TEXTURE_FILTER_CONTROL_EXT = 34048u;
        public const uint GL_TEXTURE_LOD_BIAS_EXT = 34049u;
        public const uint GL_MIRRORED_REPEAT_ARB = 33648u;
        public const uint GL_BUFFER_SIZE_ARB = 34660u;
        public const uint GL_BUFFER_USAGE_ARB = 34661u;
        public const uint GL_ARRAY_BUFFER_ARB = 34962u;
        public const uint GL_ELEMENT_ARRAY_BUFFER_ARB = 34963u;
        public const uint GL_ARRAY_BUFFER_BINDING_ARB = 34964u;
        public const uint GL_ELEMENT_ARRAY_BUFFER_BINDING_ARB = 34965u;
        public const uint GL_VERTEX_ARRAY_BUFFER_BINDING_ARB = 34966u;
        public const uint GL_NORMAL_ARRAY_BUFFER_BINDING_ARB = 34967u;
        public const uint GL_COLOR_ARRAY_BUFFER_BINDING_ARB = 34968u;
        public const uint GL_INDEX_ARRAY_BUFFER_BINDING_ARB = 34969u;
        public const uint GL_TEXTURE_COORD_ARRAY_BUFFER_BINDING_ARB = 34970u;
        public const uint GL_EDGE_FLAG_ARRAY_BUFFER_BINDING_ARB = 34971u;
        public const uint GL_SECONDARY_COLOR_ARRAY_BUFFER_BINDING_ARB = 34972u;
        public const uint GL_FOG_COORDINATE_ARRAY_BUFFER_BINDING_ARB = 34973u;
        public const uint GL_WEIGHT_ARRAY_BUFFER_BINDING_ARB = 34974u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING_ARB = 34975u;
        public const uint GL_READ_ONLY_ARB = 35000u;
        public const uint GL_WRITE_ONLY_ARB = 35001u;
        public const uint GL_READ_WRITE_ARB = 35002u;
        public const uint GL_BUFFER_ACCESS_ARB = 35003u;
        public const uint GL_BUFFER_MAPPED_ARB = 35004u;
        public const uint GL_BUFFER_MAP_POINTER_ARB = 35005u;
        public const uint GL_STREAM_DRAW_ARB = 35040u;
        public const uint GL_STREAM_READ_ARB = 35041u;
        public const uint GL_STREAM_COPY_ARB = 35042u;
        public const uint GL_STATIC_DRAW_ARB = 35044u;
        public const uint GL_STATIC_READ_ARB = 35045u;
        public const uint GL_STATIC_COPY_ARB = 35046u;
        public const uint GL_DYNAMIC_DRAW_ARB = 35048u;
        public const uint GL_DYNAMIC_READ_ARB = 35049u;
        public const uint GL_DYNAMIC_COPY_ARB = 35050u;
        public const uint GL_QUERY_COUNTER_BITS_ARB = 34916u;
        public const uint GL_CURRENT_QUERY_ARB = 34917u;
        public const uint GL_QUERY_RESULT_ARB = 34918u;
        public const uint GL_QUERY_RESULT_AVAILABLE_ARB = 34919u;
        public const uint GL_SAMPLES_PASSED_ARB = 35092u;
        public const uint GL_ANY_SAMPLES_PASSED = 35887u;
        public const uint GL_PROGRAM_OBJECT_ARB = 35648u;
        public const uint GL_SHADER_OBJECT_ARB = 35656u;
        public const uint GL_OBJECT_TYPE_ARB = 35662u;
        public const uint GL_OBJECT_SUBTYPE_ARB = 35663u;
        public const uint GL_FLOAT_VEC2_ARB = 35664u;
        public const uint GL_FLOAT_VEC3_ARB = 35665u;
        public const uint GL_FLOAT_VEC4_ARB = 35666u;
        public const uint GL_INT_VEC2_ARB = 35667u;
        public const uint GL_INT_VEC3_ARB = 35668u;
        public const uint GL_INT_VEC4_ARB = 35669u;
        public const uint GL_BOOL_ARB = 35670u;
        public const uint GL_BOOL_VEC2_ARB = 35671u;
        public const uint GL_BOOL_VEC3_ARB = 35672u;
        public const uint GL_BOOL_VEC4_ARB = 35673u;
        public const uint GL_FLOAT_MAT2_ARB = 35674u;
        public const uint GL_FLOAT_MAT3_ARB = 35675u;
        public const uint GL_FLOAT_MAT4_ARB = 35676u;
        public const uint GL_SAMPLER_1D_ARB = 35677u;
        public const uint GL_SAMPLER_2D_ARB = 35678u;
        public const uint GL_SAMPLER_3D_ARB = 35679u;
        public const uint GL_SAMPLER_CUBE_ARB = 35680u;
        public const uint GL_SAMPLER_1D_SHADOW_ARB = 35681u;
        public const uint GL_SAMPLER_2D_SHADOW_ARB = 35682u;
        public const uint GL_SAMPLER_2D_RECT_ARB = 35683u;
        public const uint GL_SAMPLER_2D_RECT_SHADOW_ARB = 35684u;
        public const uint GL_OBJECT_DELETE_STATUS_ARB = 35712u;
        public const uint GL_OBJECT_COMPILE_STATUS_ARB = 35713u;
        public const uint GL_OBJECT_LINK_STATUS_ARB = 35714u;
        public const uint GL_OBJECT_VALIDATE_STATUS_ARB = 35715u;
        public const uint GL_OBJECT_INFO_LOG_LENGTH_ARB = 35716u;
        public const uint GL_OBJECT_ATTACHED_OBJECTS_ARB = 35717u;
        public const uint GL_OBJECT_ACTIVE_UNIFORMS_ARB = 35718u;
        public const uint GL_OBJECT_ACTIVE_UNIFORM_MAX_LENGTH_ARB = 35719u;
        public const uint GL_OBJECT_SHADER_SOURCE_LENGTH_ARB = 35720u;
        public const uint GL_COLOR_SUM_ARB = 33880u;
        public const uint GL_VERTEX_PROGRAM_ARB = 34336u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_ENABLED_ARB = 34338u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_SIZE_ARB = 34339u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_STRIDE_ARB = 34340u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_TYPE_ARB = 34341u;
        public const uint GL_CURRENT_VERTEX_ATTRIB_ARB = 34342u;
        public const uint GL_PROGRAM_LENGTH_ARB = 34343u;
        public const uint GL_PROGRAM_STRING_ARB = 34344u;
        public const uint GL_MAX_PROGRAM_MATRIX_STACK_DEPTH_ARB = 34350u;
        public const uint GL_MAX_PROGRAM_MATRICES_ARB = 34351u;
        public const uint GL_CURRENT_MATRIX_STACK_DEPTH_ARB = 34368u;
        public const uint GL_CURRENT_MATRIX_ARB = 34369u;
        public const uint GL_VERTEX_PROGRAM_POINT_SIZE_ARB = 34370u;
        public const uint GL_VERTEX_PROGRAM_TWO_SIDE_ARB = 34371u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_POINTER_ARB = 34373u;
        public const uint GL_PROGRAM_ERROR_POSITION_ARB = 34379u;
        public const uint GL_PROGRAM_BINDING_ARB = 34423u;
        public const uint GL_MAX_VERTEX_ATTRIBS_ARB = 34921u;
        public const uint GL_VERTEX_ATTRIB_ARRAY_NORMALIZED_ARB = 34922u;
        public const uint GL_PROGRAM_ERROR_STRING_ARB = 34932u;
        public const uint GL_PROGRAM_FORMAT_ASCII_ARB = 34933u;
        public const uint GL_PROGRAM_FORMAT_ARB = 34934u;
        public const uint GL_PROGRAM_INSTRUCTIONS_ARB = 34976u;
        public const uint GL_MAX_PROGRAM_INSTRUCTIONS_ARB = 34977u;
        public const uint GL_PROGRAM_NATIVE_INSTRUCTIONS_ARB = 34978u;
        public const uint GL_MAX_PROGRAM_NATIVE_INSTRUCTIONS_ARB = 34979u;
        public const uint GL_PROGRAM_TEMPORARIES_ARB = 34980u;
        public const uint GL_MAX_PROGRAM_TEMPORARIES_ARB = 34981u;
        public const uint GL_PROGRAM_NATIVE_TEMPORARIES_ARB = 34982u;
        public const uint GL_MAX_PROGRAM_NATIVE_TEMPORARIES_ARB = 34983u;
        public const uint GL_PROGRAM_PARAMETERS_ARB = 34984u;
        public const uint GL_MAX_PROGRAM_PARAMETERS_ARB = 34985u;
        public const uint GL_PROGRAM_NATIVE_PARAMETERS_ARB = 34986u;
        public const uint GL_MAX_PROGRAM_NATIVE_PARAMETERS_ARB = 34987u;
        public const uint GL_PROGRAM_ATTRIBS_ARB = 34988u;
        public const uint GL_MAX_PROGRAM_ATTRIBS_ARB = 34989u;
        public const uint GL_PROGRAM_NATIVE_ATTRIBS_ARB = 34990u;
        public const uint GL_MAX_PROGRAM_NATIVE_ATTRIBS_ARB = 34991u;
        public const uint GL_PROGRAM_ADDRESS_REGISTERS_ARB = 34992u;
        public const uint GL_MAX_PROGRAM_ADDRESS_REGISTERS_ARB = 34993u;
        public const uint GL_PROGRAM_NATIVE_ADDRESS_REGISTERS_ARB = 34994u;
        public const uint GL_MAX_PROGRAM_NATIVE_ADDRESS_REGISTERS_ARB = 34995u;
        public const uint GL_MAX_PROGRAM_LOCAL_PARAMETERS_ARB = 34996u;
        public const uint GL_MAX_PROGRAM_ENV_PARAMETERS_ARB = 34997u;
        public const uint GL_PROGRAM_UNDER_NATIVE_LIMITS_ARB = 34998u;
        public const uint GL_TRANSPOSE_CURRENT_MATRIX_ARB = 34999u;
        public const uint GL_MATRIX0_ARB = 35008u;
        public const uint GL_MATRIX1_ARB = 35009u;
        public const uint GL_MATRIX2_ARB = 35010u;
        public const uint GL_MATRIX3_ARB = 35011u;
        public const uint GL_MATRIX4_ARB = 35012u;
        public const uint GL_MATRIX5_ARB = 35013u;
        public const uint GL_MATRIX6_ARB = 35014u;
        public const uint GL_MATRIX7_ARB = 35015u;
        public const uint GL_MATRIX8_ARB = 35016u;
        public const uint GL_MATRIX9_ARB = 35017u;
        public const uint GL_MATRIX10_ARB = 35018u;
        public const uint GL_MATRIX11_ARB = 35019u;
        public const uint GL_MATRIX12_ARB = 35020u;
        public const uint GL_MATRIX13_ARB = 35021u;
        public const uint GL_MATRIX14_ARB = 35022u;
        public const uint GL_MATRIX15_ARB = 35023u;
        public const uint GL_MATRIX16_ARB = 35024u;
        public const uint GL_MATRIX17_ARB = 35025u;
        public const uint GL_MATRIX18_ARB = 35026u;
        public const uint GL_MATRIX19_ARB = 35027u;
        public const uint GL_MATRIX20_ARB = 35028u;
        public const uint GL_MATRIX21_ARB = 35029u;
        public const uint GL_MATRIX22_ARB = 35030u;
        public const uint GL_MATRIX23_ARB = 35031u;
        public const uint GL_MATRIX24_ARB = 35032u;
        public const uint GL_MATRIX25_ARB = 35033u;
        public const uint GL_MATRIX26_ARB = 35034u;
        public const uint GL_MATRIX27_ARB = 35035u;
        public const uint GL_MATRIX28_ARB = 35036u;
        public const uint GL_MATRIX29_ARB = 35037u;
        public const uint GL_MATRIX30_ARB = 35038u;
        public const uint GL_MATRIX31_ARB = 35039u;
        public const uint GL_VERTEX_SHADER_ARB = 35633u;
        public const uint GL_MAX_VERTEX_UNIFORM_COMPONENTS_ARB = 35658u;
        public const uint GL_MAX_VARYING_FLOATS_ARB = 35659u;
        public const uint GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS_ARB = 35660u;
        public const uint GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS_ARB = 35661u;
        public const uint GL_OBJECT_ACTIVE_ATTRIBUTES_ARB = 35721u;
        public const uint GL_OBJECT_ACTIVE_ATTRIBUTE_MAX_LENGTH_ARB = 35722u;
        public const uint GL_FRAGMENT_SHADER_ARB = 35632u;
        public const uint GL_MAX_FRAGMENT_UNIFORM_COMPONENTS_ARB = 35657u;
        public const uint GL_FRAGMENT_SHADER_DERIVATIVE_HINT_ARB = 35723u;
        public const uint GL_MAX_DRAW_BUFFERS_ARB = 34852u;
        public const uint GL_DRAW_BUFFER0_ARB = 34853u;
        public const uint GL_DRAW_BUFFER1_ARB = 34854u;
        public const uint GL_DRAW_BUFFER2_ARB = 34855u;
        public const uint GL_DRAW_BUFFER3_ARB = 34856u;
        public const uint GL_DRAW_BUFFER4_ARB = 34857u;
        public const uint GL_DRAW_BUFFER5_ARB = 34858u;
        public const uint GL_DRAW_BUFFER6_ARB = 34859u;
        public const uint GL_DRAW_BUFFER7_ARB = 34860u;
        public const uint GL_DRAW_BUFFER8_ARB = 34861u;
        public const uint GL_DRAW_BUFFER9_ARB = 34862u;
        public const uint GL_DRAW_BUFFER10_ARB = 34863u;
        public const uint GL_DRAW_BUFFER11_ARB = 34864u;
        public const uint GL_DRAW_BUFFER12_ARB = 34865u;
        public const uint GL_DRAW_BUFFER13_ARB = 34866u;
        public const uint GL_DRAW_BUFFER14_ARB = 34867u;
        public const uint GL_DRAW_BUFFER15_ARB = 34868u;
        public const uint GL_TEXTURE_RECTANGLE_ARB = 34037u;
        public const uint GL_TEXTURE_BINDING_RECTANGLE_ARB = 34038u;
        public const uint GL_PROXY_TEXTURE_RECTANGLE_ARB = 34039u;
        public const uint GL_MAX_RECTANGLE_TEXTURE_SIZE_ARB = 34040u;
        public const uint GL_POINT_SPRITE_ARB = 34913u;
        public const uint GL_COORD_REPLACE_ARB = 34914u;
        public const uint GL_TEXTURE_RED_TYPE_ARB = 35856u;
        public const uint GL_TEXTURE_GREEN_TYPE_ARB = 35857u;
        public const uint GL_TEXTURE_BLUE_TYPE_ARB = 35858u;
        public const uint GL_TEXTURE_ALPHA_TYPE_ARB = 35859u;
        public const uint GL_TEXTURE_LUMINANCE_TYPE_ARB = 35860u;
        public const uint GL_TEXTURE_INTENSITY_TYPE_ARB = 35861u;
        public const uint GL_TEXTURE_DEPTH_TYPE_ARB = 35862u;
        public const uint GL_UNSIGNED_NORMALIZED_ARB = 35863u;
        public const uint GL_RGBA32F_ARB = 34836u;
        public const uint GL_RGB32F_ARB = 34837u;
        public const uint GL_ALPHA32F_ARB = 34838u;
        public const uint GL_INTENSITY32F_ARB = 34839u;
        public const uint GL_LUMINANCE32F_ARB = 34840u;
        public const uint GL_LUMINANCE_ALPHA32F_ARB = 34841u;
        public const uint GL_RGBA16F_ARB = 34842u;
        public const uint GL_RGB16F_ARB = 34843u;
        public const uint GL_ALPHA16F_ARB = 34844u;
        public const uint GL_INTENSITY16F_ARB = 34845u;
        public const uint GL_LUMINANCE16F_ARB = 34846u;
        public const uint GL_LUMINANCE_ALPHA16F_ARB = 34847u;
        public const uint GL_BLEND_EQUATION_RGB_EXT = 32777u;
        public const uint GL_BLEND_EQUATION_ALPHA_EXT = 34877u;
        public const uint GL_STENCIL_TEST_TWO_SIDE_EXT = 32777u;
        public const uint GL_ACTIVE_STENCIL_FACE_EXT = 34877u;
        public const uint GL_PIXEL_PACK_BUFFER_ARB = 35051u;
        public const uint GL_PIXEL_UNPACK_BUFFER_ARB = 35052u;
        public const uint GL_PIXEL_PACK_BUFFER_BINDING_ARB = 35053u;
        public const uint GL_PIXEL_UNPACK_BUFFER_BINDING_ARB = 35055u;
        public const uint GL_SRGB_EXT = 35904u;
        public const uint GL_SRGB8_EXT = 35905u;
        public const uint GL_SRGB_ALPHA_EXT = 35906u;
        public const uint GL_SRGB8_ALPHA8_EXT = 35907u;
        public const uint GL_SLUMINANCE_ALPHA_EXT = 35908u;
        public const uint GL_SLUMINANCE8_ALPHA8_EXT = 35909u;
        public const uint GL_SLUMINANCE_EXT = 35910u;
        public const uint GL_SLUMINANCE8_EXT = 35911u;
        public const uint GL_COMPRESSED_SRGB_EXT = 35912u;
        public const uint GL_COMPRESSED_SRGB_ALPHA_EXT = 35913u;
        public const uint GL_COMPRESSED_SLUMINANCE_EXT = 35914u;
        public const uint GL_COMPRESSED_SLUMINANCE_ALPHA_EXT = 35915u;
        public const uint GL_COMPRESSED_SRGB_S3TC_DXT1_EXT = 35916u;
        public const uint GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT1_EXT = 35917u;
        public const uint GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT3_EXT = 35918u;
        public const uint GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT5_EXT = 35919u;
        public const uint GL_INVALID_FRAMEBUFFER_OPERATION_EXT = 1286u;
        public const uint GL_MAX_RENDERBUFFER_SIZE_EXT = 34024u;
        public const uint GL_FRAMEBUFFER_BINDING_EXT = 36006u;
        public const uint GL_RENDERBUFFER_BINDING_EXT = 36007u;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE_EXT = 36048u;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_OBJECT_NAME_EXT = 36049u;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL_EXT = 36050u;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE_EXT = 36051u;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_3D_ZOFFSET_EXT = 36052u;
        public const uint GL_FRAMEBUFFER_COMPLETE_EXT = 36053u;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT_EXT = 36054u;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT_EXT = 36055u;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_DIMENSIONS_EXT = 36057u;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_FORMATS_EXT = 36058u;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER_EXT = 36059u;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER_EXT = 36060u;
        public const uint GL_FRAMEBUFFER_UNSUPPORTED_EXT = 36061u;
        public const uint GL_MAX_COLOR_ATTACHMENTS_EXT = 36063u;
        public const uint GL_COLOR_ATTACHMENT0_EXT = 36064u;
        public const uint GL_COLOR_ATTACHMENT1_EXT = 36065u;
        public const uint GL_COLOR_ATTACHMENT2_EXT = 36066u;
        public const uint GL_COLOR_ATTACHMENT3_EXT = 36067u;
        public const uint GL_COLOR_ATTACHMENT4_EXT = 36068u;
        public const uint GL_COLOR_ATTACHMENT5_EXT = 36069u;
        public const uint GL_COLOR_ATTACHMENT6_EXT = 36070u;
        public const uint GL_COLOR_ATTACHMENT7_EXT = 36071u;
        public const uint GL_COLOR_ATTACHMENT8_EXT = 36072u;
        public const uint GL_COLOR_ATTACHMENT9_EXT = 36073u;
        public const uint GL_COLOR_ATTACHMENT10_EXT = 36074u;
        public const uint GL_COLOR_ATTACHMENT11_EXT = 36075u;
        public const uint GL_COLOR_ATTACHMENT12_EXT = 36076u;
        public const uint GL_COLOR_ATTACHMENT13_EXT = 36077u;
        public const uint GL_COLOR_ATTACHMENT14_EXT = 36078u;
        public const uint GL_COLOR_ATTACHMENT15_EXT = 36079u;
        public const uint GL_DEPTH_ATTACHMENT_EXT = 36096u;
        public const uint GL_STENCIL_ATTACHMENT_EXT = 36128u;
        public const uint GL_FRAMEBUFFER_EXT = 36160u;
        public const uint GL_RENDERBUFFER_EXT = 36161u;
        public const uint GL_RENDERBUFFER_WIDTH_EXT = 36162u;
        public const uint GL_RENDERBUFFER_HEIGHT_EXT = 36163u;
        public const uint GL_RENDERBUFFER_INTERNAL_FORMAT_EXT = 36164u;
        public const uint GL_STENCIL_INDEX1_EXT = 36166u;
        public const uint GL_STENCIL_INDEX4_EXT = 36167u;
        public const uint GL_STENCIL_INDEX8_EXT = 36168u;
        public const uint GL_STENCIL_INDEX16_EXT = 36169u;
        public const uint GL_RENDERBUFFER_RED_SIZE_EXT = 36176u;
        public const uint GL_RENDERBUFFER_GREEN_SIZE_EXT = 36177u;
        public const uint GL_RENDERBUFFER_BLUE_SIZE_EXT = 36178u;
        public const uint GL_RENDERBUFFER_ALPHA_SIZE_EXT = 36179u;
        public const uint GL_RENDERBUFFER_DEPTH_SIZE_EXT = 36180u;
        public const uint GL_RENDERBUFFER_STENCIL_SIZE_EXT = 36181u;
        public const uint GL_RENDERBUFFER_SAMPLES_EXT = 36011u;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE_EXT = 36182u;
        public const uint GL_MAX_SAMPLES_EXT = 36183u;
        public const uint GL_VERTEX_ARRAY_BINDING = 34229u;
        public const uint GL_FRAMEBUFFER_SRGB_EXT = 36281u;
        public const uint GL_FRAMEBUFFER_SRGB_CAPABLE_EXT = 36282u;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_EXT = 35982u;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_START_EXT = 35972u;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_SIZE_EXT = 35973u;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_BINDING_EXT = 35983u;
        public const uint GL_INTERLEAVED_ATTRIBS_EXT = 35980u;
        public const uint GL_SEPARATE_ATTRIBS_EXT = 35981u;
        public const uint GL_PRIMITIVES_GENERATED_EXT = 35975u;
        public const uint GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN_EXT = 35976u;
        public const uint GL_RASTERIZER_DISCARD_EXT = 35977u;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS_EXT = 35978u;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS_EXT = 35979u;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS_EXT = 35968u;
        public const uint GL_TRANSFORM_FEEDBACK_VARYINGS_EXT = 35971u;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_MODE_EXT = 35967u;
        public const uint GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH_EXT = 35958u;
        public const int WGL_CONTEXT_MAJOR_VERSION_ARB = 8337;
        public const int WGL_CONTEXT_MINOR_VERSION_ARB = 8338;
        public const int WGL_CONTEXT_LAYER_PLANE_ARB = 8339;
        public const int WGL_CONTEXT_FLAGS_ARB = 8340;
        public const int WGL_CONTEXT_PROFILE_MASK_ARB = 37158;
        public const int WGL_CONTEXT_DEBUG_BIT_ARB = 1;
        public const int WGL_CONTEXT_FORWARD_COMPATIBLE_BIT_ARB = 2;
        public const int WGL_CONTEXT_CORE_PROFILE_BIT_ARB = 1;
        public const int WGL_CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB = 2;
        public const int ERROR_INVALID_VERSION_ARB = 8341;
        public const int ERROR_INVALID_PROFILE_ARB = 8342;
        private static readonly Dictionary<string, Delegate> OglExtensions = new Dictionary<string, Delegate>();
        public uint FOG_SPECULAR_TEXTURE_WIN = 33004u;

        [DllImport("opengl32.dll")]
        public static extern void glAccum(uint op, float value);

        [DllImport("opengl32.dll")]
        public static extern void glAlphaFunc(uint func, float ref_notkeword);

        [DllImport("opengl32.dll")]
        public static extern byte glAreTexturesResident(int n, uint[] textures, byte[] residences);

        [DllImport("opengl32.dll")]
        public static extern void glArrayElement(int i);

        [DllImport("opengl32.dll")]
        public static extern void glBegin(uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glBindTexture(uint target, uint texture);

        [DllImport("opengl32.dll")]
        public static extern void glBitmap(int width, int height, float xorig, float yorig, float xmove, float ymove,
            byte[] bitmap);

        [DllImport("opengl32.dll")]
        public static extern void glBlendFunc(uint sfactor, uint dfactor);

        [DllImport("opengl32.dll")]
        public static extern void glCallList(uint list);

        [DllImport("opengl32.dll")]
        public static extern void glCallLists(int n, uint type, IntPtr lists);

        [DllImport("opengl32.dll")]
        public static extern void glCallLists(int n, uint type, uint[] lists);

        [DllImport("opengl32.dll")]
        public static extern void glCallLists(int n, uint type, byte[] lists);

        [DllImport("opengl32.dll")]
        public static extern void glClear(uint mask);

        [DllImport("opengl32.dll")]
        public static extern void glClearAccum(float red, float green, float blue, float alpha);

        [DllImport("opengl32.dll")]
        public static extern void glClearColor(float red, float green, float blue, float alpha);

        [DllImport("opengl32.dll")]
        public static extern void glClearDepth(double depth);

        [DllImport("opengl32.dll")]
        public static extern void glClearIndex(float c);

        [DllImport("opengl32.dll")]
        public static extern void glClearStencil(int s);

        [DllImport("opengl32.dll")]
        public static extern void glClipPlane(uint plane, double[] equation);

        [DllImport("opengl32.dll")]
        public static extern void glColor3b(byte red, byte green, byte blue);

        [DllImport("opengl32.dll")]
        public static extern void glColor3bv(byte[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor3d(double red, double green, double blue);

        [DllImport("opengl32.dll")]
        public static extern void glColor3dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor3f(float red, float green, float blue);

        [DllImport("opengl32.dll")]
        public static extern void glColor3fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor3i(int red, int green, int blue);

        [DllImport("opengl32.dll")]
        public static extern void glColor3iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor3s(short red, short green, short blue);

        [DllImport("opengl32.dll")]
        public static extern void glColor3sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor3ub(byte red, byte green, byte blue);

        [DllImport("opengl32.dll")]
        public static extern void glColor3ubv(byte[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor3ui(uint red, uint green, uint blue);

        [DllImport("opengl32.dll")]
        public static extern void glColor3uiv(uint[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor3us(ushort red, ushort green, ushort blue);

        [DllImport("opengl32.dll")]
        public static extern void glColor3usv(ushort[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor4b(byte red, byte green, byte blue, byte alpha);

        [DllImport("opengl32.dll")]
        public static extern void glColor4bv(byte[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor4d(double red, double green, double blue, double alpha);

        [DllImport("opengl32.dll")]
        public static extern void glColor4dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor4f(float red, float green, float blue, float alpha);

        [DllImport("opengl32.dll")]
        public static extern void glColor4fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor4i(int red, int green, int blue, int alpha);

        [DllImport("opengl32.dll")]
        public static extern void glColor4iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor4s(short red, short green, short blue, short alpha);

        [DllImport("opengl32.dll")]
        public static extern void glColor4sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor4ub(byte red, byte green, byte blue, byte alpha);

        [DllImport("opengl32.dll")]
        public static extern void glColor4ubv(byte[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor4ui(uint red, uint green, uint blue, uint alpha);

        [DllImport("opengl32.dll")]
        public static extern void glColor4uiv(uint[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColor4us(ushort red, ushort green, ushort blue, ushort alpha);

        [DllImport("opengl32.dll")]
        public static extern void glColor4usv(ushort[] v);

        [DllImport("opengl32.dll")]
        public static extern void glColorMask(byte red, byte green, byte blue, byte alpha);

        [DllImport("opengl32.dll")]
        public static extern void glColorMaterial(uint face, uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glColorPointer(int size, uint type, int stride, IntPtr pointer);

        [DllImport("opengl32.dll")]
        public static extern void glCopyPixels(int x, int y, int width, int height, uint type);

        [DllImport("opengl32.dll")]
        public static extern void glCopyTexImage1D(uint target, int level, uint internalFormat, int x, int y, int width,
            int border);

        [DllImport("opengl32.dll")]
        public static extern void glCopyTexImage2D(uint target, int level, uint internalFormat, int x, int y, int width,
            int height, int border);

        [DllImport("opengl32.dll")]
        public static extern void glCopyTexSubImage1D(uint target, int level, int xoffset, int x, int y, int width);

        [DllImport("opengl32.dll")]
        public static extern void glCopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y,
            int width, int height);

        [DllImport("opengl32.dll")]
        public static extern void glCullFace(uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glDeleteLists(uint list, int range);

        [DllImport("opengl32.dll")]
        public static extern void glDeleteTextures(int n, uint[] textures);

        [DllImport("opengl32.dll")]
        public static extern void glDepthFunc(uint func);

        [DllImport("opengl32.dll")]
        public static extern void glDepthMask(byte flag);

        [DllImport("opengl32.dll")]
        public static extern void glDepthRange(double zNear, double zFar);

        [DllImport("opengl32.dll")]
        public static extern void glDisable(uint cap);

        [DllImport("opengl32.dll")]
        public static extern void glDisableClientState(uint array);

        [DllImport("opengl32.dll")]
        public static extern void glDrawArrays(uint mode, int first, int count);

        [DllImport("opengl32.dll")]
        public static extern void glDrawBuffer(uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glDrawElements(uint mode, int count, uint type, IntPtr indices);

        [DllImport("opengl32.dll")]
        public static extern void glDrawElements(uint mode, int count, uint type, uint[] indices);

        [DllImport("opengl32.dll")]
        public static extern void glDrawPixels(int width, int height, uint format, uint type, float[] pixels);

        [DllImport("opengl32.dll")]
        public static extern void glDrawPixels(int width, int height, uint format, uint type, uint[] pixels);

        [DllImport("opengl32.dll")]
        public static extern void glDrawPixels(int width, int height, uint format, uint type, ushort[] pixels);

        [DllImport("opengl32.dll")]
        public static extern void glDrawPixels(int width, int height, uint format, uint type, byte[] pixels);

        [DllImport("opengl32.dll")]
        public static extern void glDrawPixels(int width, int height, uint format, uint type, IntPtr pixels);

        [DllImport("opengl32.dll")]
        public static extern void glEdgeFlag(byte flag);

        [DllImport("opengl32.dll")]
        public static extern void glEdgeFlagPointer(int stride, int[] pointer);

        [DllImport("opengl32.dll")]
        public static extern void glEdgeFlagv(byte[] flag);

        [DllImport("opengl32.dll")]
        public static extern void glEnable(uint cap);

        [DllImport("opengl32.dll")]
        public static extern void glEnableClientState(uint array);

        [DllImport("opengl32.dll")]
        public static extern void glEnd();

        [DllImport("opengl32.dll")]
        public static extern void glEndList();

        [DllImport("opengl32.dll")]
        public static extern void glEvalCoord1d(double u);

        [DllImport("opengl32.dll")]
        public static extern void glEvalCoord1dv(double[] u);

        [DllImport("opengl32.dll")]
        public static extern void glEvalCoord1f(float u);

        [DllImport("opengl32.dll")]
        public static extern void glEvalCoord1fv(float[] u);

        [DllImport("opengl32.dll")]
        public static extern void glEvalCoord2d(double u, double v);

        [DllImport("opengl32.dll")]
        public static extern void glEvalCoord2dv(double[] u);

        [DllImport("opengl32.dll")]
        public static extern void glEvalCoord2f(float u, float v);

        [DllImport("opengl32.dll")]
        public static extern void glEvalCoord2fv(float[] u);

        [DllImport("opengl32.dll")]
        public static extern void glEvalMesh1(uint mode, int i1, int i2);

        [DllImport("opengl32.dll")]
        public static extern void glEvalMesh2(uint mode, int i1, int i2, int j1, int j2);

        [DllImport("opengl32.dll")]
        public static extern void glEvalPoint1(int i);

        [DllImport("opengl32.dll")]
        public static extern void glEvalPoint2(int i, int j);

        [DllImport("opengl32.dll")]
        public static extern void glFeedbackBuffer(int size, uint type, float[] buffer);

        [DllImport("opengl32.dll")]
        public static extern void glFinish();

        [DllImport("opengl32.dll")]
        public static extern void glFlush();

        [DllImport("opengl32.dll")]
        public static extern void glFogf(uint pname, float param);

        [DllImport("opengl32.dll")]
        public static extern void glFogfv(uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glFogi(uint pname, int param);

        [DllImport("opengl32.dll")]
        public static extern void glFogiv(uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glFrontFace(uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glFrustum(double left, double right, double bottom, double top, double zNear,
            double zFar);

        [DllImport("opengl32.dll")]
        public static extern uint glGenLists(int range);

        [DllImport("opengl32.dll")]
        public static extern void glGenTextures(int n, uint[] textures);

        [DllImport("opengl32.dll")]
        public static extern void glGetBooleanv(uint pname, byte[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetClipPlane(uint plane, double[] equation);

        [DllImport("opengl32.dll")]
        public static extern void glGetDoublev(uint pname, double[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern uint glGetError();

        [DllImport("opengl32.dll")]
        public static extern void glGetFloatv(uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetIntegerv(uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetLightfv(uint light, uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetLightiv(uint light, uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetMapdv(uint target, uint query, double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glGetMapfv(uint target, uint query, float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glGetMapiv(uint target, uint query, int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glGetMaterialfv(uint face, uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetMaterialiv(uint face, uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetPixelMapfv(uint map, float[] values);

        [DllImport("opengl32.dll")]
        public static extern void glGetPixelMapuiv(uint map, uint[] values);

        [DllImport("opengl32.dll")]
        public static extern void glGetPixelMapusv(uint map, ushort[] values);

        [DllImport("opengl32.dll")]
        public static extern void glGetPointerv(uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetPolygonStipple(byte[] mask);

        [DllImport("opengl32.dll")]
        public static extern unsafe sbyte* glGetString(uint name);

        [DllImport("opengl32.dll")]
        public static extern void glGetTexEnvfv(uint target, uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetTexEnviv(uint target, uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetTexGendv(uint coord, uint pname, double[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetTexGenfv(uint coord, uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetTexGeniv(uint coord, uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetTexImage(uint target, int level, uint format, uint type, int[] pixels);

        [DllImport("opengl32.dll")]
        public static extern void glGetTexLevelParameterfv(uint target, int level, uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetTexLevelParameteriv(uint target, int level, uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetTexParameterfv(uint target, uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glGetTexParameteriv(uint target, uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glHint(uint target, uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glIndexMask(uint mask);

        [DllImport("opengl32.dll")]
        public static extern void glIndexPointer(uint type, int stride, int[] pointer);

        [DllImport("opengl32.dll")]
        public static extern void glIndexd(double c);

        [DllImport("opengl32.dll")]
        public static extern void glIndexdv(double[] c);

        [DllImport("opengl32.dll")]
        public static extern void glIndexf(float c);

        [DllImport("opengl32.dll")]
        public static extern void glIndexfv(float[] c);

        [DllImport("opengl32.dll")]
        public static extern void glIndexi(int c);

        [DllImport("opengl32.dll")]
        public static extern void glIndexiv(int[] c);

        [DllImport("opengl32.dll")]
        public static extern void glIndexs(short c);

        [DllImport("opengl32.dll")]
        public static extern void glIndexsv(short[] c);

        [DllImport("opengl32.dll")]
        public static extern void glIndexub(byte c);

        [DllImport("opengl32.dll")]
        public static extern void glIndexubv(byte[] c);

        [DllImport("opengl32.dll")]
        public static extern void glInitNames();

        [DllImport("opengl32.dll")]
        public static extern void glInterleavedArrays(uint format, int stride, int[] pointer);

        [DllImport("opengl32.dll")]
        public static extern byte glIsEnabled(uint cap);

        [DllImport("opengl32.dll")]
        public static extern byte glIsList(uint list);

        [DllImport("opengl32.dll")]
        public static extern byte glIsTexture(uint texture);

        [DllImport("opengl32.dll")]
        public static extern void glLightModelf(uint pname, float param);

        [DllImport("opengl32.dll")]
        public static extern void glLightModelfv(uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glLightModeli(uint pname, int param);

        [DllImport("opengl32.dll")]
        public static extern void glLightModeliv(uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glLightf(uint light, uint pname, float param);

        [DllImport("opengl32.dll")]
        public static extern void glLightfv(uint light, uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glLighti(uint light, uint pname, int param);

        [DllImport("opengl32.dll")]
        public static extern void glLightiv(uint light, uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glLineStipple(int factor, ushort pattern);

        [DllImport("opengl32.dll")]
        public static extern void glLineWidth(float width);

        [DllImport("opengl32.dll")]
        public static extern void glListBase(uint base_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glLoadIdentity();

        [DllImport("opengl32.dll")]
        public static extern void glLoadMatrixd(double[] m);

        [DllImport("opengl32.dll")]
        public static extern void glLoadMatrixf(float[] m);

        [DllImport("opengl32.dll")]
        public static extern void glLoadName(uint name);

        [DllImport("opengl32.dll")]
        public static extern void glLogicOp(uint opcode);

        [DllImport("opengl32.dll")]
        public static extern void glMap1d(uint target, double u1, double u2, int stride, int order, double[] points);

        [DllImport("opengl32.dll")]
        public static extern void glMap1f(uint target, float u1, float u2, int stride, int order, float[] points);

        [DllImport("opengl32.dll")]
        public static extern void glMap2d(uint target, double u1, double u2, int ustride, int uorder, double v1,
            double v2, int vstride, int vorder, double[] points);

        [DllImport("opengl32.dll")]
        public static extern void glMap2f(uint target, float u1, float u2, int ustride, int uorder, float v1, float v2,
            int vstride, int vorder, float[] points);

        [DllImport("opengl32.dll")]
        public static extern void glMapGrid1d(int un, double u1, double u2);

        [DllImport("opengl32.dll")]
        public static extern void glMapGrid1f(int un, float u1, float u2);

        [DllImport("opengl32.dll")]
        public static extern void glMapGrid2d(int un, double u1, double u2, int vn, double v1, double v2);

        [DllImport("opengl32.dll")]
        public static extern void glMapGrid2f(int un, float u1, float u2, int vn, float v1, float v2);

        [DllImport("opengl32.dll")]
        public static extern void glMaterialf(uint face, uint pname, float param);

        [DllImport("opengl32.dll")]
        public static extern void glMaterialfv(uint face, uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glMateriali(uint face, uint pname, int param);

        [DllImport("opengl32.dll")]
        public static extern void glMaterialiv(uint face, uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glMatrixMode(uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glMultMatrixd(double[] m);

        [DllImport("opengl32.dll")]
        public static extern void glMultMatrixf(float[] m);

        [DllImport("opengl32.dll")]
        public static extern void glNewList(uint list, uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glNormal3b(byte nx, byte ny, byte nz);

        [DllImport("opengl32.dll")]
        public static extern void glNormal3bv(byte[] v);

        [DllImport("opengl32.dll")]
        public static extern void glNormal3d(double nx, double ny, double nz);

        [DllImport("opengl32.dll")]
        public static extern void glNormal3dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glNormal3f(float nx, float ny, float nz);

        [DllImport("opengl32.dll")]
        public static extern void glNormal3fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glNormal3i(int nx, int ny, int nz);

        [DllImport("opengl32.dll")]
        public static extern void glNormal3iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glNormal3s(short nx, short ny, short nz);

        [DllImport("opengl32.dll")]
        public static extern void glNormal3sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glNormalPointer(uint type, int stride, IntPtr pointer);

        [DllImport("opengl32.dll")]
        public static extern void glNormalPointer(uint type, int stride, float[] pointer);

        [DllImport("opengl32.dll")]
        public static extern void glOrtho(double left, double right, double bottom, double top, double zNear,
            double zFar);

        [DllImport("opengl32.dll")]
        public static extern void glPassThrough(float token);

        [DllImport("opengl32.dll")]
        public static extern void glPixelMapfv(uint map, int mapsize, float[] values);

        [DllImport("opengl32.dll")]
        public static extern void glPixelMapuiv(uint map, int mapsize, uint[] values);

        [DllImport("opengl32.dll")]
        public static extern void glPixelMapusv(uint map, int mapsize, ushort[] values);

        [DllImport("opengl32.dll")]
        public static extern void glPixelStoref(uint pname, float param);

        [DllImport("opengl32.dll")]
        public static extern void glPixelStorei(uint pname, int param);

        [DllImport("opengl32.dll")]
        public static extern void glPixelTransferf(uint pname, float param);

        [DllImport("opengl32.dll")]
        public static extern void glPixelTransferi(uint pname, int param);

        [DllImport("opengl32.dll")]
        public static extern void glPixelZoom(float xfactor, float yfactor);

        [DllImport("opengl32.dll")]
        public static extern void glPointSize(float size);

        [DllImport("opengl32.dll")]
        public static extern void glPolygonMode(uint face, uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glPolygonOffset(float factor, float units);

        [DllImport("opengl32.dll")]
        public static extern void glPolygonStipple(byte[] mask);

        [DllImport("opengl32.dll")]
        public static extern void glPopAttrib();

        [DllImport("opengl32.dll")]
        public static extern void glPopClientAttrib();

        [DllImport("opengl32.dll")]
        public static extern void glPopMatrix();

        [DllImport("opengl32.dll")]
        public static extern void glPopName();

        [DllImport("opengl32.dll")]
        public static extern void glPrioritizeTextures(int n, uint[] textures, float[] priorities);

        [DllImport("opengl32.dll")]
        public static extern void glPushAttrib(uint mask);

        [DllImport("opengl32.dll")]
        public static extern void glPushClientAttrib(uint mask);

        [DllImport("opengl32.dll")]
        public static extern void glPushMatrix();

        [DllImport("opengl32.dll")]
        public static extern void glPushName(uint name);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos2d(double x, double y);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos2dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos2f(float x, float y);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos2fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos2i(int x, int y);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos2iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos2s(short x, short y);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos2sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos3d(double x, double y, double z);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos3dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos3f(float x, float y, float z);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos3fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos3i(int x, int y, int z);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos3iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos3s(short x, short y, short z);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos3sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos4d(double x, double y, double z, double w);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos4dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos4f(float x, float y, float z, float w);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos4fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos4i(int x, int y, int z, int w);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos4iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos4s(short x, short y, short z, short w);

        [DllImport("opengl32.dll")]
        public static extern void glRasterPos4sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glReadBuffer(uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glReadPixels(int x, int y, int width, int height, uint format, uint type,
            byte[] pixels);

        [DllImport("opengl32.dll")]
        public static extern void glReadPixels(int x, int y, int width, int height, uint format, uint type,
            IntPtr pixels);

        [DllImport("opengl32.dll")]
        public static extern void glRectd(double x1, double y1, double x2, double y2);

        [DllImport("opengl32.dll")]
        public static extern void glRectdv(double[] v1, double[] v2);

        [DllImport("opengl32.dll")]
        public static extern void glRectf(float x1, float y1, float x2, float y2);

        [DllImport("opengl32.dll")]
        public static extern void glRectfv(float[] v1, float[] v2);

        [DllImport("opengl32.dll")]
        public static extern void glRecti(int x1, int y1, int x2, int y2);

        [DllImport("opengl32.dll")]
        public static extern void glRectiv(int[] v1, int[] v2);

        [DllImport("opengl32.dll")]
        public static extern void glRects(short x1, short y1, short x2, short y2);

        [DllImport("opengl32.dll")]
        public static extern void glRectsv(short[] v1, short[] v2);

        [DllImport("opengl32.dll")]
        public static extern int glRenderMode(uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glRotated(double angle, double x, double y, double z);

        [DllImport("opengl32.dll")]
        public static extern void glRotatef(float angle, float x, float y, float z);

        [DllImport("opengl32.dll")]
        public static extern void glScaled(double x, double y, double z);

        [DllImport("opengl32.dll")]
        public static extern void glScalef(float x, float y, float z);

        [DllImport("opengl32.dll")]
        public static extern void glScissor(int x, int y, int width, int height);

        [DllImport("opengl32.dll")]
        public static extern void glSelectBuffer(int size, uint[] buffer);

        [DllImport("opengl32.dll")]
        public static extern void glShadeModel(uint mode);

        [DllImport("opengl32.dll")]
        public static extern void glStencilFunc(uint func, int ref_notkeword, uint mask);

        [DllImport("opengl32.dll")]
        public static extern void glStencilMask(uint mask);

        [DllImport("opengl32.dll")]
        public static extern void glStencilOp(uint fail, uint zfail, uint zpass);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord1d(double s);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord1dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord1f(float s);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord1fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord1i(int s);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord1iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord1s(short s);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord1sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord2d(double s, double t);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord2dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord2f(float s, float t);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord2fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord2i(int s, int t);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord2iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord2s(short s, short t);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord2sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord3d(double s, double t, double r);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord3dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord3f(float s, float t, float r);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord3fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord3i(int s, int t, int r);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord3iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord3s(short s, short t, short r);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord3sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord4d(double s, double t, double r, double q);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord4dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord4f(float s, float t, float r, float q);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord4fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord4i(int s, int t, int r, int q);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord4iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord4s(short s, short t, short r, short q);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoord4sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoordPointer(int size, uint type, int stride, IntPtr pointer);

        [DllImport("opengl32.dll")]
        public static extern void glTexCoordPointer(int size, uint type, int stride, float[] pointer);

        [DllImport("opengl32.dll")]
        public static extern void glTexEnvf(uint target, uint pname, float param);

        [DllImport("opengl32.dll")]
        public static extern void glTexEnvfv(uint target, uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glTexEnvi(uint target, uint pname, int param);

        [DllImport("opengl32.dll")]
        public static extern void glTexEnviv(uint target, uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glTexGend(uint coord, uint pname, double param);

        [DllImport("opengl32.dll")]
        public static extern void glTexGendv(uint coord, uint pname, double[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glTexGenf(uint coord, uint pname, float param);

        [DllImport("opengl32.dll")]
        public static extern void glTexGenfv(uint coord, uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glTexGeni(uint coord, uint pname, int param);

        [DllImport("opengl32.dll")]
        public static extern void glTexGeniv(uint coord, uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glTexImage1D(uint target, int level, uint internalformat, int width, int border,
            uint format, uint type, byte[] pixels);

        [DllImport("opengl32.dll")]
        public static extern void glTexImage2D(uint target, int level, uint internalformat, int width, int height,
            int border, uint format, uint type, byte[] pixels);

        [DllImport("opengl32.dll")]
        public static extern void glTexImage2D(uint target, int level, uint internalformat, int width, int height,
            int border, uint format, uint type, IntPtr pixels);

        [DllImport("opengl32.dll")]
        public static extern void glTexParameterf(uint target, uint pname, float param);

        [DllImport("opengl32.dll")]
        public static extern void glTexParameterfv(uint target, uint pname, float[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glTexParameteri(uint target, uint pname, int param);

        [DllImport("opengl32.dll")]
        public static extern void glTexParameteriv(uint target, uint pname, int[] params_notkeyword);

        [DllImport("opengl32.dll")]
        public static extern void glTexSubImage1D(uint target, int level, int xoffset, int width, uint format, uint type,
            int[] pixels);

        [DllImport("opengl32.dll")]
        public static extern void glTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width,
            int height, uint format, uint type, int[] pixels);

        [DllImport("opengl32.dll")]
        public static extern void glTranslated(double x, double y, double z);

        [DllImport("opengl32.dll")]
        public static extern void glTranslatef(float x, float y, float z);

        [DllImport("opengl32.dll")]
        public static extern void glVertex2d(double x, double y);

        [DllImport("opengl32.dll")]
        public static extern void glVertex2dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glVertex2f(float x, float y);

        [DllImport("opengl32.dll")]
        public static extern void glVertex2fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glVertex2i(int x, int y);

        [DllImport("opengl32.dll")]
        public static extern void glVertex2iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glVertex2s(short x, short y);

        [DllImport("opengl32.dll")]
        public static extern void glVertex2sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glVertex3d(double x, double y, double z);

        [DllImport("opengl32.dll")]
        public static extern void glVertex3dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glVertex3f(float x, float y, float z);

        [DllImport("opengl32.dll")]
        public static extern void glVertex3fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glVertex3i(int x, int y, int z);

        [DllImport("opengl32.dll")]
        public static extern void glVertex3iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glVertex3s(short x, short y, short z);

        [DllImport("opengl32.dll")]
        public static extern void glVertex3sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glVertex4d(double x, double y, double z, double w);

        [DllImport("opengl32.dll")]
        public static extern void glVertex4dv(double[] v);

        [DllImport("opengl32.dll")]
        public static extern void glVertex4f(float x, float y, float z, float w);

        [DllImport("opengl32.dll")]
        public static extern void glVertex4fv(float[] v);

        [DllImport("opengl32.dll")]
        public static extern void glVertex4i(int x, int y, int z, int w);

        [DllImport("opengl32.dll")]
        public static extern void glVertex4iv(int[] v);

        [DllImport("opengl32.dll")]
        public static extern void glVertex4s(short x, short y, short z, short w);

        [DllImport("opengl32.dll")]
        public static extern void glVertex4sv(short[] v);

        [DllImport("opengl32.dll")]
        public static extern void glVertexPointer(int size, uint type, int stride, IntPtr pointer);

        [DllImport("opengl32.dll")]
        public static extern void glVertexPointer(int size, uint type, int stride, short[] pointer);

        [DllImport("opengl32.dll")]
        public static extern void glVertexPointer(int size, uint type, int stride, int[] pointer);

        [DllImport("opengl32.dll")]
        public static extern void glVertexPointer(int size, uint type, int stride, float[] pointer);

        [DllImport("opengl32.dll")]
        public static extern void glVertexPointer(int size, uint type, int stride, double[] pointer);

        [DllImport("opengl32.dll")]
        public static extern void glViewport(int x, int y, int width, int height);

        /// <summary>
        /// Creates ContextAttribsARB.
        /// </summary>
        /// <param name="dc">The DeviceContext.</param>
        /// <param name="hShareContext">The HShareContext.</param>
        /// <param name="attribList">The AttribList.</param>
        /// <returns>IntPtr.</returns>
        public static IntPtr CreateContextAttribsARB(IntPtr dc, IntPtr hShareContext, int[] attribList)
        {
            return (IntPtr) InvokeExtension<wglCreateContextAttribsARB>(dc, hShareContext, attribList);
        }

        /// <summary>
        /// Invokes an extension method.
        /// </summary>
        /// <typeparam name="T">The DelegateType.</typeparam>
        /// <param name="args">The Parameters.</param>
        /// <returns>Object.</returns>
        public static object InvokeExtension<T>(params object[] args)
        {
#if Windows
            Type delType = typeof (T);
            string delName = delType.Name;

            Delegate exDelegate;

            if (!OglExtensions.ContainsKey(delName))
            {
                IntPtr proc = NativeMethods.wglGetProcAddress(delName);
                if (proc == IntPtr.Zero)
                    throw new InvalidOperationException(string.Format("The extension method {0} does not exists.",
                        delName));

                exDelegate = Marshal.GetDelegateForFunctionPointer(proc, delType);

                OglExtensions.Add(delName, exDelegate);
            }

            exDelegate = OglExtensions[delName];
            object result;

            try
            {
                result = exDelegate.DynamicInvoke(args);
            }
            catch
            {
                throw new InvalidOperationException(string.Format("The extension method {0} can not be invoked.",
                    delName));
            }

            return result;
#else
            throw new NotSupportedException();
#endif
        }
    }
}